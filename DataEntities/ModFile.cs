using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class ModFile
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string ModCreator { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public List<string> FileNames { get; set; } = new List<string>();
        [JsonIgnore]
        public string ModPath { get; set; }
    }
}
