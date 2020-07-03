using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public abstract class FileList
    {
        [JsonIgnore]
        public string FileName { get; set; }
        [JsonIgnore]
        public FileTypes FileType { get; set; }

        public enum FileTypes
        {
            Game,
            Names
        }

        public abstract string GetValue();
        public abstract bool AllowedToAdd(Dictionary<string, object> options);
    }

    public class NameFile : FileList
    {
        public DataEntities.Character.GenderEnum? Gender
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public override bool AllowedToAdd(Dictionary<string, object> options)
        {
            var ret = false;

            object genderEnum = null;

            options.TryGetValue("Gender", out genderEnum);

            var gender = (DataEntities.Character.GenderEnum?)genderEnum;

            if (this.Gender == gender)
            {
                ret = true;
            }

            return ret;
        }

        public override string GetValue()
        {
            return this.Name;
        }
    }
}
