using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEntities
{
    public class Game
    {
        public Player Player { get; set; } = new Player();
        public List<Faction> Factions { get; set; } = new List<Faction>();
        public List<FileLists.RaceFile> Races { get; set; } = new List<FileLists.RaceFile>();
    }

    public class Player : Character
    {

        
    }

    public class Character
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public JobTitleEnum? JobTitle { get; set; }

        private string _JobTitleName
        {
            get; set;
        }

        public string JobTitleName {
            get
            {
                if (string.IsNullOrEmpty(this._JobTitleName))
                {
                    this._JobTitleName = this.JobTitle.ToString();
                }

                return this._JobTitleName;
            }
            set 
            {
                this._JobTitleName = value;
            }
        }

        public Location Location { get; set; }

        public Inventory Inventory { get; set; }

        public Character Father { get; set; }

        public Character Mother { get; set; }

        public List<Character> Children { get; set; } = new List<Character>();
        public FileLists.RaceFile Race { get; set; }
        public GenderEnum Gender { get; set; }

        public string GenderOther { get; set; }

        public string GenderName
        {
            get
            {
                if (Gender == GenderEnum.Female)
                {
                    return "Female";
                }
                else if (Gender == GenderEnum.Male)
                {
                    return "Male";
                }
                else
                {
                    return GenderOther;
                }
            }
        }

        public enum GenderEnum
        {
            Male,
            Female,
            Other
        }

        public enum JobTitleEnum
        {
            Blacksmith,
            Merchant
        }

        public List<FactionLevel> FactionLevels { get; set; } = new List<FactionLevel>();
    }

    public class Inventory
    {
        public List<InventorySlot> InventorySlots { get; set; } = new List<InventorySlot>(); //Will eventually have to overrite the adds so that it will combine same item in item slots

        public class InventorySlot
        {
            public Item Item { get; set; }
            public int Amount { get; set; }
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public static class ItemList
    {
        private static List<Item> Items = new List<Item>()
        {
            new Item() { Name = "Gold", Description = "Shiny metal that is used for trading" },
            new Item() { Name = "Apple" },
            new Item() { Name = "Hammer" }
        };

        public static Item GetItemByName(string name)
        {
            Item ret = new Item() { Name = "ERROR" };

            var search = Items.Where(x => x.Name.ToUpper() == name.ToUpper());

            if (search != null && search.Count() > 0)
            {
                ret = search.FirstOrDefault();
            }

            return ret;
        }
    }

    public class Race
    {
        public string Name { get; set; }
        public int MaxAge { get; set; }
        public int AverageAgeBeforeDeath { get; set; }
    }

    public class FactionLevel
    {
        public Faction Faction { get; set; }
        public DutyEnum Duty { get; set; }
        public int Status { get; set; }

        public enum DutyEnum
        {
            Guard,
            King,
            Queen,
            Jester,
            Servant,
            None
        }
    }


    //WorldGen

    public class City
    {
        public string Name { get; set; }
        public Faction Faction { get; set; }
    }

    public class Faction
    {
        public string Name { get; set; }
    }
}
