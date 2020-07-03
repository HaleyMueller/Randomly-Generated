using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonHandler
{
    public class GameHandler
    {
        private DataEntities.Game Game;

        public GameHandler()
        {
            //Where are of the files are saved to
            BusinessImplementation.FileHandler._GamePath = AppDomain.CurrentDomain.BaseDirectory;
            BusinessImplementation.FileHandler.UpdateGamePaths();

            //Creates and updates files if needed
            using (var fileHandlerBLL = new BusinessImplementation.FileHandler(Game))
            {
                fileHandlerBLL.UpdateAllFiles();

                this.Game = fileHandlerBLL.Game; //This is retrieved from the fileHandlerBLL
            }

            //this.Game = BusinessImplementation.FileHandler.LoadGameFile();
        }

        public DataEntities.ReturnResult SaveGame()
        {
            using (var fileHandlerBLL = new BusinessImplementation.FileHandler(this.Game))
            {
                fileHandlerBLL.SaveGameFile(this.Game);

                return new DataEntities.ReturnResult()
                {
                    new DataEntities.ReturnResult.Result()
                    {
                         ResultMessage = "Successfully saved"
                    }
                };
            }
        }

        public List<DataEntities.ModFile> GetModFiles()
        {
            using (var modHandlerBLL = new BusinessImplementation.ModHandler(this.Game))
            {
                return modHandlerBLL.GetAllMods();
            }
        }

        public DataEntities.ReturnResult GetLocation()
        {
            using (var locationBLL = new BusinessImplementation.Location(this.Game))
            {
                string locationName = locationBLL.GetCurrentLocation().Name;

                return new DataEntities.ReturnResult()
                {
                    new DataEntities.ReturnResult.Result()
                    {
                         ResultMessage = "You are in " + locationName
                    }
                };
            }
        }

        public DataEntities.ReturnResult Input(string input)
        {
            using (var processInputBLL = new BusinessImplementation.ProcessInput(this.Game))
            {
                return processInputBLL.Input(input);
            }
        }
    }
}
