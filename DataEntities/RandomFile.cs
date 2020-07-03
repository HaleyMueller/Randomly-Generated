using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class RandomFile
    {
        public class RandNames
        {
            public class RandName : IEqualityComparer<RandName>
            {
                public DataEntities.Character.GenderEnum Gender
                {
                    get; set;
                }

                public string Name
                {
                    get; set;
                }

                public bool Equals(RandName x, RandName y)
                {
                    return x.Gender == y.Gender && x.Name == y.Name;
                }

                public int GetHashCode(RandName obj)
                {
                    return obj.Gender.GetHashCode() ^ obj.Name.GetHashCode();
                }
            }

            public static List<RandName> GetDefault()
            {
                return new List<RandName>()
                {
                    new RandName()
                    {
                        Name = "Bob",
                        Gender = Character.GenderEnum.Male
                    },
                    new RandName()
                    {
                        Name = "Haley",
                        Gender = Character.GenderEnum.Female
                    },
                    new RandName()
                    {
                        Name = "miki",
                        Gender = Character.GenderEnum.Male
                    },
                    new RandName()
                    {
                        Name = "name",
                        Gender = Character.GenderEnum.Female
                    }
                };
            }
        }
    }
}
