﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BusinessImplementation
{
    public class GameFileHandler : GameBase
    {
        public GameFileHandler(DataEntities.Game game)
        {
            this.Game = game;
        }

        //public string _GameDirectory = Path.Combine(FileHandler.GamePath, "Files/Save");
        //public string _FilesDirectory = Path.Combine(FileHandler.GamePath, "Files");

        public void CreateGameFolder()
        {
            Directory.CreateDirectory(FileHandler._GameDirectory);
        }

        public DataEntities.Game GetGame()
        {
            var ret = new DataEntities.Game();

            //var modPath = Path.Combine(FileHandler.GamePath, "mods", directory);
            var jsonPath = Path.Combine(FileHandler._GameDirectory, "game.json");

            if (File.Exists(jsonPath))
            {
                var cereal = Newtonsoft.Json.JsonConvert.DeserializeObject<DataEntities.Game>(File.ReadAllText(jsonPath));

                ret = cereal;
            }
            else //Couldn't find file
            {
                ret = new DataEntities.Game();
            }

            return ret;
        }

        public List<DataEntities.FileList> GetGameFileList(DataEntities.FileList.FileTypes fileType)
        {
            List<DataEntities.FileList> ret = new List<DataEntities.FileList>();

            var fileName = FileHandler.GetFileNameFromFileType(fileType);

            var filePath = Path.Combine(FileHandler._FilesDirectory, fileName);


            var cereal = File.ReadAllText(filePath);

            var type = FileHandler.GetTypeFromFileType(fileType);

            Type genericType = typeof(List<>).MakeGenericType(new[] { type });
            IList list = (IList)Activator.CreateInstance(genericType);
            var listType = list.GetType();

            var decereal = Newtonsoft.Json.JsonConvert.DeserializeObject(cereal, listType);

            if (decereal is List<DataEntities.NameFile>)
            {
                var s = (List<DataEntities.NameFile>)decereal;
                s.ForEach(x => { x.FileName = "Game"; x.FileType = DataEntities.FileList.FileTypes.Names; });

                ret.AddRange(s);
            }

            return ret;
        }

        public List<DataEntities.FileList> GetGameFileLists()
        {
            List<DataEntities.FileList> ret = new List<DataEntities.FileList>();

            var enums = Enum.GetValues(typeof(DataEntities.FileList.FileTypes)).Cast<DataEntities.FileList.FileTypes>().ToList();

            foreach (var enumObj in enums)
            {
                if (enumObj != DataEntities.FileList.FileTypes.Game)
                    ret.AddRange(GetGameFileList(enumObj));
            }

            return ret;
        }

        public static bool DoesSaveExist()
        {
            return System.IO.File.Exists(Path.Combine(FileHandler._GameDirectory, FileHandler.GetFileNameFromFileType(DataEntities.FileList.FileTypes.Game)));
        }
    }
}
