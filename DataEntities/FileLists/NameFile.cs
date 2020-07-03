using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.FileLists
{
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
