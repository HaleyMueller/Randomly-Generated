using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace BusinessImplementation
{
    public class FileHandler : GameBase
    {
        public static List<DataEntities.FileList> _FileLists = new List<DataEntities.FileList>();

        public static string _GamePath = "";
        public static string _GameDirectory = "";
        public static string _FilesDirectory = "";
        public static string _ModPath = "";

        public FileHandler (DataEntities.Game game)
        {
            this.Game = game;
        }

        public static void UpdateGamePaths()
        {
            _GameDirectory = Path.Combine(FileHandler._GamePath, "files/save");
            _FilesDirectory = Path.Combine(FileHandler._GamePath, "files");
            _ModPath = Path.Combine(FileHandler._GamePath, "mods");
        }

        public void UpdateAllFiles()
        {
            bool hasSaveFile = GameFileHandler.DoesSaveExist();

            var gameHandlerBLL = new BusinessImplementation.GameFileHandler(this.Game);
            var modHandlerBLL = new BusinessImplementation.ModHandler(this.Game);

            this.Game = gameHandlerBLL.GetGame();

            //Create all necessary files
            modHandlerBLL.CreateModsFolder();

            //Get File Lists
            var modsFileList = modHandlerBLL.LoadMods();
            var gameFileList = gameHandlerBLL.GetGameFileLists();

            _FileLists.AddRange(modsFileList);
            _FileLists.AddRange(gameFileList);

            //Save first time stuff in game
            if (hasSaveFile == false)
            {
                using (var generateRandomBLL = new BusinessImplementation.GenerateRandomObject(this.Game))
                {
                    this.Game.Races = generateRandomBLL.CreateRaces(1);

                    this.Game.Player = generateRandomBLL.GetPlayer();
                }

                //Testing
                this.Game.Player.Location = new DataEntities.Location()
                {
                    Name = "Morrowind",
                    Destinations = new List<DataEntities.Destination>()
                    {
                        new DataEntities.Destination()
                        {
                            Name = "bar"
                        },
                        new DataEntities.Destination()
                        {
                            Name = "fat man"
                        }
                    }
                };

                SaveGameFile(Game);
            }
        }

        public static DataEntities.FileList GetRandom(DataEntities.FileList.FileTypes fileTypes, Dictionary<string, object> options)
        {
            DataEntities.FileList ret = null;

            var filesLookingFor = new List<DataEntities.FileList>();

            var fileTypeType = GetTypeFromFileType(fileTypes);

            foreach (var file in _FileLists)
            {
                if (file.GetType() == fileTypeType)
                {
                    if (file.AllowedToAdd(options))
                    {
                        filesLookingFor.Add(file);
                    }
                }
            }

            Random rand = new Random();
            var r = rand.Next(0, filesLookingFor.Count);

            if (filesLookingFor.Any()) //Check if any were found
            {
                ret = filesLookingFor[r];
            }
            else //Error
            {

            }

            return ret;
        }

        public static System.Type GetTypeFromFileType(DataEntities.FileList.FileTypes fileTypes)
        {
            System.Type ret = null;

            switch (fileTypes)
            {
                case DataEntities.FileList.FileTypes.Names:
                    ret = typeof(DataEntities.NameFile);
                    break;
            }

            return ret;
        }

        public static string GetFileNameFromFileType(DataEntities.FileList.FileTypes fileTypes)
        {
            switch (fileTypes)
            {
                case DataEntities.FileList.FileTypes.Game:
                    return "game.json";
                case DataEntities.FileList.FileTypes.Names:
                    return "names.json";
            }

            return "ThisShouldntBeHere.txt";
        }

        //public static string ReadFromFile(string fileName, FileTypes? fileTypes)
        //{
        //    string path = System.IO.Path.Combine(GamePath, fileName);

        //    if (!System.IO.File.Exists(path))
        //    {
        //        CreateDefaultFile(path, fileTypes);
        //    }

        //    return System.IO.File.ReadAllText(path);
        //}

        ////Hits everytime the game loads
        //private static void CreateDefaultFile(string path, FileTypes? fileTypes)
        //{
        //    string fileContents = null;

        //    switch (fileTypes)
        //    {
        //        case FileTypes.RandomNames:
        //            //fileContents = RandomHandler.MergeRandom(RandomHandler.RandomTypes.Name);
        //            break;
        //    }

        //    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false))
        //    {
        //        sw.Write(fileContents);
        //    }
        //}



        //public static DataEntities.Game LoadGameFile()
        //{
        //    string fileContents = "";

        //    using (System.IO.StreamReader sw = new System.IO.StreamReader(System.IO.Path.Combine(GamePath, GetFileNameFromFileType(DataEntities.FileList.FileTypes.Game))))
        //    {
        //        fileContents = sw.ReadToEnd();
        //    }

        //    return Newtonsoft.Json.JsonConvert.DeserializeObject<DataEntities.Game>(fileContents);
        //}

        public void SaveGameFile(DataEntities.Game game)
        {
            string fileContents = "";

            fileContents = Newtonsoft.Json.JsonConvert.SerializeObject(game, Newtonsoft.Json.Formatting.Indented);

            var path = System.IO.Path.Combine(_GameDirectory, GetFileNameFromFileType(DataEntities.FileList.FileTypes.Game));

            var gameFileHander = new BusinessImplementation.GameFileHandler(this.Game);
            gameFileHander.CreateGameFolder();

            if (File.Exists(path) == false) //Create game file if it doesn't exist
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write("");
                }
            }

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path))
            {
                sw.Write(fileContents);
            }
        }

        //public static void CreateFile(string path)
        //{
        //    if (!System.IO.File.Exists(path))
        //    {
        //        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path))
        //        {
        //            sw.Write("");
        //        }
        //    }
        //}

        //public static void UpdateFile(string path, string fileContents, bool append)
        //{
        //    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path, append))
        //    {
        //        sw.Write(fileContents);
        //    }
        //}
    }
}
