using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessImplementation
{
    public class GenerateRandomObject : GameBase
    {
        public GenerateRandomObject(DataEntities.Game game)
        {
            this.Game = game;
        }

        //Creates

        public List<DataEntities.Race> CreateRaces(int howMany)
        {
            var ret = new List<DataEntities.Race>();

            for (int i = 0; i < howMany; i++)
            {
                ret.Add(CreateRace());
            }

            return ret;
        }

        public DataEntities.Race CreateRace()
        {
            var ret = new DataEntities.Race(); //Change these to pull from file in one object

            ret.Name = "Human";
            ret.MaxAge = 100;
            ret.AverageAgeBeforeDeath = 80;

            return ret;
        }

        //Gets
        public DataEntities.Player GetPlayer() //Fix this to use the character function
        {
            DataEntities.Player ret;

            var gender = GetGender();

            var genderOther = "";
            if (gender == DataEntities.Character.GenderEnum.Other)
            {
                //Get random gender name from file
                genderOther = "Attack Helicopter";
            }

            var name = (DataEntities.NameFile)BusinessImplementation.FileHandler.GetRandom(DataEntities.FileList.FileTypes.Names, new Dictionary<string, object>() { { "Gender", gender } });
            var age = GetAge();
            var jobTitle = GetJobTitle(false);
            var factionLevels = GetFactionLevels();
            var inventory = GetInventory(jobTitle);
            var race = GetRace();

            ret = new DataEntities.Player()
            {
                Name = name.Name,
                Gender = gender,
                GenderOther = genderOther,
                Age = age,
                JobTitle = jobTitle,
                FactionLevels = factionLevels,
                Inventory = inventory,
                Race = race
            };

            return ret;
        }

        public DataEntities.Character GetCharacter()
        {
            DataEntities.Character ret;

            var gender = GetGender();

            var genderOther = "";
            if (gender == DataEntities.Character.GenderEnum.Other)
            {
                //Get random gender name from file
                genderOther = "Attack Helicopter";
            }

            var name = (DataEntities.NameFile)BusinessImplementation.FileHandler.GetRandom(DataEntities.FileList.FileTypes.Names, new Dictionary<string, object>() { { "Gender", gender } });
            var age = GetAge();
            var jobTitle = GetJobTitle(false);
            var factionLevels = GetFactionLevels();
            var inventory = GetInventory(jobTitle);
            var race = GetRace();

            ret = new DataEntities.Character()
            {
                Name = name.Name,
                Gender = gender,
                GenderOther = genderOther,
                Age = age,
                JobTitle = jobTitle,
                FactionLevels = factionLevels,
                Inventory = inventory,
                Race = race
            };

            return ret;
        }

        public List<DataEntities.FactionLevel> GetFactionLevels()
        {
            var ret = new List<DataEntities.FactionLevel>();

            foreach (var faction in this.Game.Factions)
            {
                var factionLevel = new DataEntities.FactionLevel();

                factionLevel.Faction = faction;
                factionLevel.Duty = DataEntities.FactionLevel.DutyEnum.None; //Hard coded for now
                factionLevel.Status = 10; //Hard coded for now

                ret.Add(factionLevel);
            }

            return ret;
        }

        public DataEntities.Race GetRace()
        {
            var ret = new DataEntities.Race();

            Random rand = new Random();
            var i = rand.Next(0, this.Game.Races.Count);

            ret = this.Game.Races[i];

            return ret;
        }

        //Based off of stuff like faction status / Job Title
        public DataEntities.Inventory GetInventory(DataEntities.Character.JobTitleEnum? jobTitle)
        {
            var ret = new DataEntities.Inventory();

            var inventorySlots = new List<DataEntities.Inventory.InventorySlot>();

            Random rand = new Random();

            //Items based on job title
            if (jobTitle != null)
            {
                var i = rand.Next(0, 100);

                var ii = rand.Next(1, 4);

                switch (jobTitle)
                {
                    case DataEntities.Character.JobTitleEnum.Blacksmith:
                        if (i < 10)
                        {
                            inventorySlots.Add(new DataEntities.Inventory.InventorySlot()
                            {
                                Item = DataEntities.ItemList.GetItemByName("Hammer"),
                                Amount = 1
                            });
                        }
                        break;
                    case DataEntities.Character.JobTitleEnum.Merchant:
                        if (i < 80)
                        {
                            inventorySlots.Add(new DataEntities.Inventory.InventorySlot()
                            {
                                Item = DataEntities.ItemList.GetItemByName("Apple"),
                                Amount = ii
                            });
                        }
                        break;
                }
            }

            var gold = rand.Next(0, 15);

            if (gold > 0)
            {
                inventorySlots.Add(new DataEntities.Inventory.InventorySlot()
                {
                    Item = DataEntities.ItemList.GetItemByName("Gold"),
                    Amount = gold
                });
            }

            ret.InventorySlots = inventorySlots;

            return ret;
        }

        public DataEntities.Character.GenderEnum GetGender()
        {
            DataEntities.Character.GenderEnum ret = DataEntities.Character.GenderEnum.Male;

            Random rand = new Random();
            var i = rand.Next(0, 100);

            if (i == 69)
            {
                ret = DataEntities.Character.GenderEnum.Other;
            }
            else if (i % 2 == 0)
            {
                ret = DataEntities.Character.GenderEnum.Female;
            }

            return ret;
        }

        //Do this based off race
        public int GetAge()
        {
            Random rand = new Random();
            var i = rand.Next(1, 100);

            return i;
        }

        public DataEntities.Character.JobTitleEnum? GetJobTitle(bool alwaysGetTitle)
        {
            DataEntities.Character.JobTitleEnum? ret;

            Array values = Enum.GetValues(typeof(DataEntities.Character.JobTitleEnum));
            Random random = new Random();
            ret = (DataEntities.Character.JobTitleEnum)values.GetValue(random.Next(values.Length));

            if (alwaysGetTitle == false)
            {
                var i = random.Next(0, 100);

                if (i < 80)
                {
                    ret = null;
                }
            }

            return ret;
        }

        //In the future we can use the game inference system to get more related values
        public DataEntities.Faction GetFaction()
        {
            var ret = new DataEntities.Faction();



            return ret;
        }
    }
}
