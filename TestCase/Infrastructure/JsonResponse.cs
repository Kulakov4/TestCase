using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestCase.Infrastructure
{
    public static class JsonResponse
    {
        public static JsonResult New<T>(T value)
        {
            return new JsonResult(new SuccessResult<T>(value));
        }
    }
}
