using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.FileLists
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
            Names,
            Genders,
            Races
        }

        public abstract string GetValue();
        public abstract bool AllowedToAdd(Dictionary<string, object> options);
        public abstract List<FileList> GetDefaultValues();
    }
}
