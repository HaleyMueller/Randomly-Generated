using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.FileLists
{
    public class GenderFile : FileList
    {
        public string Gender
        {
            get; set;
        }

        public override bool AllowedToAdd(Dictionary<string, object> options)
        {
            var ret = true;

            return ret;
        }

        public override string GetValue()
        {
            return this.Gender;
        }

        public override List<FileList> GetDefaultValues()
        {
            var ret = new List<FileList>();

            ret.Add(new GenderFile() { Gender = "Attack Helicopter" });
            ret.Add(new GenderFile() { Gender = "Non Binary" });
            ret.Add(new GenderFile() { Gender = "Trans Female" });
            ret.Add(new GenderFile() { Gender = "Trans Male" });

            return ret;
        }
    }
}
