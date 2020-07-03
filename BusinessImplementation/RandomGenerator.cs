using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessImplementation
{
    public static class RandomHandler
    {
        /// <summary>
        /// Gets a random string from a specified file
        /// </summary>
        //public static string GetRandom(RandomTypes randomTypes, Dictionary<string, object> options)
        //{
        //    string fileContents = "";
        //    string randomString = "";

        //    switch (randomTypes)
        //    {
        //        case RandomTypes.Name:
        //            fileContents = FileHandler.ReadFromFile(FileHandler.GetFileNameFromFileType(FileHandler.FileTypes.RandomNames), FileHandler.FileTypes.RandomNames);

        //            object genderEnum = null;

        //            options.TryGetValue("Gender", out genderEnum);

        //            randomString = RandomGenerator.GetRandomName(fileContents, (DataEntities.Character.GenderEnum?)genderEnum);
        //            break;
        //    }

        //    return randomString;
        //}

        /// <summary>
        /// Merges custom random types into the same file without overwriting user made ones
        /// </summary>
        //public static string MergeRandom(RandomTypes randomTypes)
        //{
        //    string fileContents = "";

        //    switch (randomTypes)
        //    {
        //        case RandomTypes.Name:
        //            FileHandler.CreateFile(System.IO.Path.Combine(FileHandler.GamePath, FileHandler.GetFileNameFromFileType(FileHandler.FileTypes.RandomNames)));

        //            fileContents = FileHandler.ReadFromFile(FileHandler.GetFileNameFromFileType(FileHandler.FileTypes.RandomNames), FileHandler.FileTypes.RandomNames);

        //            if (!string.IsNullOrEmpty(fileContents))
        //            {
        //                fileContents = Newtonsoft.Json.JsonConvert.SerializeObject(RandomGenerator.MergeRandomNames(fileContents, DataEntities.RandomFile.RandNames.GetDefault()), Newtonsoft.Json.Formatting.Indented);
        //            }
        //            else
        //            {
        //                fileContents = Newtonsoft.Json.JsonConvert.SerializeObject(DataEntities.RandomFile.RandNames.GetDefault(), Newtonsoft.Json.Formatting.Indented);
        //            }
        //            break;
        //    }

        //    FileHandler.UpdateFile(System.IO.Path.Combine(FileHandler.GamePath, FileHandler.GetFileNameFromFileType(FileHandler.FileTypes.RandomNames)), fileContents, false);

        //    return fileContents;
        //}

        public enum RandomTypes
        {
            Name
        }

        //Private for a reason. Put nasty code in here
        private static class RandomGenerator
        {
            internal static string GetRandomName(string fileContents, DataEntities.Character.GenderEnum? gender)
            {
                var entity = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataEntities.RandomFile.RandNames.RandName>>(fileContents);

                var entitySearched = new List<DataEntities.RandomFile.RandNames.RandName>();

                if (gender == DataEntities.Character.GenderEnum.Female)
                {
                    entitySearched = entity.Where(x => x.Gender == DataEntities.Character.GenderEnum.Female).ToList();
                }
                else if (gender == DataEntities.Character.GenderEnum.Male)
                {
                    entitySearched = entity.Where(x => x.Gender == DataEntities.Character.GenderEnum.Male).ToList();
                }
                else
                {
                    entitySearched = entity;
                }

                var rand = new Random();
                var randIndex = rand.Next(entitySearched.Count);

                return entitySearched[randIndex].Name;
            }

            internal static List<DataEntities.RandomFile.RandNames.RandName> MergeRandomNames(string fileContents, List<DataEntities.RandomFile.RandNames.RandName> randNames)
            {
                var entity = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataEntities.RandomFile.RandNames.RandName>>(fileContents);

                entity.AddRange(randNames);

                entity = entity.Distinct(new DataEntities.RandomFile.RandNames.RandName()).ToList();

                return entity;
            }
        }
    }
}
