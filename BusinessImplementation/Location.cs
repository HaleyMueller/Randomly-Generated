using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessImplementation
{
    public class Location : GameBase
    {
        public Location(DataEntities.Game game)
        {
            this.Game = game;
        }

        public DataEntities.Location GetCurrentLocation()
        {
            return this.Game.Player.Location;
        }
    }
}
