using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class SaveData
    {
        public string lastMap;
        public string lastHero;
        public SaveData(string lastMap, string lastHero)
        {
            this.lastMap = lastMap;
            this.lastHero = lastHero;
        }
    }
}
