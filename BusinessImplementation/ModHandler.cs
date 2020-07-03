using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections;

namespace BusinessImplementation
{
    public class ModHandler : GameBase
    {
        public ModHandler(DataEntities.Game game)
        {
            this.Game = game;
        }

        public void CreateModsFolder()
        {
            Directory.CreateDirectory(FileHandler._ModPath);
        }

        public List<string> GetModFolders()
        {
            var modsPath = Path.Combine(FileHandler._ModPath);

            return Directory.GetDirectories(modsPath).ToList();
        }

        public DataEntities.ModFile GetMod(string directory)
        {
            var ret = new DataEntities.ModFile();

            //var modPath = Path.Combine(FileHandler.GamePath, "mods", directory);
            var jsonPath = Path.Combine(FileHandler._ModPath, directory, "mod.json");

            if (File.Exists(jsonPath))
            {
                var cereal = Newtonsoft.Json.JsonConvert.DeserializeObject<DataEntities.ModFile>(File.ReadAllText(jsonPath));

                var files = Directory.GetFiles(directory);

                cereal.FileNames.AddRange(files);
                cereal.ModPath = directory;

                ret = cereal;
            }
            else
            {
                //Couldn't find file
            }

            return ret;
        }

        public List<DataEntities.FileLists.FileList> GetModFileList(DataEntities.ModFile modFile, DataEntities.FileLists.FileList.FileTypes fileType)
        {
            List<DataEntities.FileLists.FileList> ret = new List<DataEntities.FileLists.FileList>();

            var fileName = FileHandler.GetFileNameFromFileType(fileType);

            var filePath = Path.Combine(modFile.ModPath, fileName);

            if (modFile.FileNames.Contains(filePath)) //If the mod has the json file
            {
                var cereal = File.ReadAllText(filePath);

                var type = FileHandler.GetTypeFromFileType(fileType);

                Type genericType = typeof(List<>).MakeGenericType(new[] { type });
                IList list = (IList)Activator.CreateInstance(genericType);
                var listType = list.GetType();

                var decereal = Newtonsoft.Json.JsonConvert.DeserializeObject(cereal, listType);

                if (decereal is List<DataEntities.FileLists.NameFile>)
                {
                    var s = (List<DataEntities.FileLists.NameFile>)decereal;
                    s.ForEach(x => { x.FileName = modFile.Name; x.FileType = DataEntities.FileLists.FileList.FileTypes.Names; });

                    ret.AddRange(s);
                }
                else if (decereal is List<DataEntities.FileLists.GenderFile>)
                {
                    var s = (List<DataEntities.FileLists.GenderFile>)decereal;
                    s.ForEach(x => { x.FileName = modFile.Name; x.FileType = DataEntities.FileLists.FileList.FileTypes.Genders; });

                    ret.AddRange(s);
                }
                else if (decereal is List<DataEntities.FileLists.RaceFile>)
                {
                    var s = (List<DataEntities.FileLists.RaceFile>)decereal;
                    s.ForEach(x => { x.FileName = modFile.Name; x.FileType = DataEntities.FileLists.FileList.FileTypes.Races; });

                    ret.AddRange(s);
                }
            }

            return ret;
        }

        public List<DataEntities.FileLists.FileList> GetModFileLists(DataEntities.ModFile modFile)
        {
            List<DataEntities.FileLists.FileList> ret = new List<DataEntities.FileLists.FileList>();

            var enums = Enum.GetValues(typeof(DataEntities.FileLists.FileList.FileTypes)).Cast<DataEntities.FileLists.FileList.FileTypes>().ToList();

            foreach (var enumObj in enums)
            {
                ret.AddRange(GetModFileList(modFile, enumObj));
            }

            return ret;
        }

        /// <summary>
        /// This loads all of the mods in the mods folder
        /// </summary>
        public List<DataEntities.FileLists.FileList> LoadMods()
        {
            var ret = new List<DataEntities.FileLists.FileList>();

            //Get all mod folder paths
            var modFolders = GetModFolders();

            //Get mod for each mod folder and add to mod list
            //var mods = new List<DataEntities.ModFile>();

            foreach (var modFolder in modFolders)
            {
                var mod = GetMod(modFolder);

                //mods.Add(mod);

                ret.AddRange(GetModFileLists(mod));
            }

            return ret;
        }

        /// <summary>
        /// This gets all of the mods in the mods folder
        /// </summary>
        public List<DataEntities.ModFile> GetAllMods()
        {
            var ret = new List<DataEntities.ModFile>();

            //Get all mod folder paths
            var modFolders = GetModFolders();

            //Get mod for each mod folder and add to mod list
            //var mods = new List<DataEntities.ModFile>();

            foreach (var modFolder in modFolders)
            {
                var mod = GetMod(modFolder);

                //mods.Add(mod);

                ret.Add(mod);
            }

            return ret;
        }
    }
}
