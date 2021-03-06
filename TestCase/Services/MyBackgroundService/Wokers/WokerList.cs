using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Interfaces;

namespace TestCase.Services.MyBackgroundService.Wokers
{
    public class WokerList : IWokerList
    {
        public List<IWoker> Wokers { get; set; }

        public WokerList()
        {
            Wokers = new List<IWoker> { new TalabatWoker(), new ZomatoWoker(), new UberWoker() };

        }
    }
}
