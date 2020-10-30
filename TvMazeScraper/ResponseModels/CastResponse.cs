using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TvMazeScraper.ResponseModels
{
    public class CastResponse
    {
        public person Person { get; set; }
        public character character { get; set; }
        public bool self { get; set; }
        public bool voice { get; set; }
    }
  
    public class person
    {
        public int id { get; set; }
        public string name { get; set; }
        public string birthday { get; set; }
    }
    public class character
    {
        public int id { get; set; }
        public string name { get; set; }
        public string birthday { get; set; }
    }
}
