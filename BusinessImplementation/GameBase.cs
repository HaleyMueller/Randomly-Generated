using System;

namespace BusinessImplementation
{
    public class GameBase : IDisposable
    {
        public DataEntities.Game Game { get; set; }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}