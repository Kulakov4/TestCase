using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Interfaces;

namespace TestCase.Services
{
    public class LogServiceArgs 
    { 
        public string FileName { get; set; }
    }

    public class LogService : ILogService
    {
        private readonly string fileName;
        
        public LogService(LogServiceArgs args)
        {
            fileName = args.FileName;
            //File.Create(fileName);
        }

        public async Task Add(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            

            await File.AppendAllTextAsync(fileName, $"{DateTime.Now} {message} {Environment.NewLine}");

            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}
