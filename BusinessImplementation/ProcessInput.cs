using System;

namespace BusinessImplementation
{
    public class ProcessInput : GameBase
    {
        public ProcessInput(DataEntities.Game game)
        {
            this.Game = game;
        }

        public DataEntities.ReturnResult Input(string input)
        {
            var ret = new DataEntities.ReturnResult();

            using (CommandHandler commandHandlerBLL = new CommandHandler(this.Game))
            {
                var result = commandHandlerBLL.ProcessCommand(input);

                ret.Add(result);
            }

            return ret;
        }
    }
}
