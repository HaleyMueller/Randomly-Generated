using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.FileLists
{
    public class RaceFile : FileList
    {
        public string Name { get; set; }
        public int MaxAge { get; set; }
        public int AverageAgeBeforeDeath { get; set; }

        public override bool AllowedToAdd(Dictionary<string, object> options)
        {
            var ret = true;

            return ret;
        }

        public override string GetValue()
        {
            return this.Name;
        }

        public override List<FileList> GetDefaultValues()
        {
            var ret = new List<FileList>();

            ret.Add(new RaceFile() { Name = "Human", MaxAge = 100, AverageAgeBeforeDeath = 80 });

            return ret;
        }
    }
}
