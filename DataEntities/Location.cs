using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class Location
    {
        public string Name { get; set; }

        public List<Destination> Destinations { get; set; } = new List<Destination>();
    }

    public class Destination
    {
        public string Name { get; set; }
    }
}
