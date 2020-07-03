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

            if (this.Gender == gender || gender == Character.GenderEnum.Other)
            {
                ret = true;
            }

            return ret;
        }

        public override string GetValue()
        {
            return this.Name;
        }

        public override List<FileList> GetDefaultValues()
        {
            var ret = new List<FileList>();

            ret.Add(new NameFile() { Name = "Bob", Gender = Character.GenderEnum.Male });
            ret.Add(new NameFile() { Name = "Sara", Gender = Character.GenderEnum.Female });

            return ret;
        }
    }
}
