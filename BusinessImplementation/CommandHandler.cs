using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessImplementation
{
    public class CommandHandler : GameBase
    {
        public CommandHandler(DataEntities.Game game)
        {
            this.Game = game;
        }

        public List<Command> commands = new List<Command>() {
            new Command(){
                Name = "HELP",
                Aliases = new List<string>() { "DUMFOUNDED" },
                Description = "Help for commands",
                Parameters = new List<Parameter>()
                {
                    new Parameter()
                    {
                        Name = "COMMAND_NAME",
                        Optional = true
                    }
                }
            },
            new Command() {
                Name = "LOCATION",
                Aliases = new List<string>(),
                Description = "Returns your player location",
                Parameters = new List<Parameter>()
            },
            new Command()
            {
                Name = "LOOK",
                Aliases = new List<string>() { "LOOK AROUND", "SEARCH" },
                Description = "Look around the current area for destinations",
                Parameters = new List<Parameter>()
            },
            new Command()
            {
                Name = "INV",
                Aliases = new List<string>() { "INVENTORY", "POCKET" },
                Description = "Items that you have",
                Parameters = new List<Parameter>()
                {
                    new Parameter()
                    {
                        Name = "ITEM_NAME",
                        Optional = true
                    }
                }
            }
        };

        public Command GetCommand(string name)
        {
            var ret = new Command();

            var found = commands.Where(x => x.Name == name.ToUpper() || x.Aliases.Contains(name.ToUpper()));

            if (found != null)
            {
                ret = found.FirstOrDefault();
            }

            return ret;
        }

        public string GetHelpCommand(Command command)
        {
            var ret = "";

            ret = $"Name: {command.Name + Environment.NewLine}" +
                $"Description: {command.Description + Environment.NewLine}" +
                (command.Aliases != null ? $"Aliases:{string.Join(", ", command.Aliases) + Environment.NewLine}" : "") +
                (command.Parameters == null | command.Parameters.Count() < 1 ? "" : $"Parameters: {string.Join(", " + Environment.NewLine, command.Parameters.Select(x => x.Name + " Optional:" + x.Optional)) + Environment.NewLine} ");

            return ret;
        }

        public DataEntities.ReturnResult.Result ProcessCommand(string input)
        {
            var ret = new DataEntities.ReturnResult.Result();

            var commandSplit = input.Split(' ');

            var prefix = commandSplit[0];

            var command = GetCommand(prefix);

            if (command == new Command() || command == null)
            {
                return ret = new DataEntities.ReturnResult.Result()
                {
                    IsError = true,
                    ResultMessage = prefix + " isn't a command. Say help for a list of commands",
                    ShowInUI = true,
                    Status = DataEntities.ReturnResult.Statuses.Success
                };
            }

            var parameterCount = commandSplit.Length - 1;

            var minimumParameterNeeded = command.Parameters.Count(x => x.Optional == false);

            if (commandSplit.Length -1 < command.Parameters.Count() && command.Name != "HELP" && minimumParameterNeeded > parameterCount)
            {
                return ret = new DataEntities.ReturnResult.Result()
                {
                    IsError = true,
                    ResultMessage = prefix + " is missing parameters",
                    ShowInUI = true,
                    Status = DataEntities.ReturnResult.Statuses.Success
                };
            }

            switch (command.Name.ToUpper())
            {
                case "HELP":

                    if (commandSplit.Length <= 1)
                    {
                        ret = new DataEntities.ReturnResult.Result()
                        {
                            IsError = false,
                            ResultMessage = string.Join("," + Environment.NewLine, commands.Select(x => x.Name)),
                            ShowInUI = true,
                            Status = DataEntities.ReturnResult.Statuses.Success
                        };
                    }
                    else
                    {
                        var helpCommand = GetCommand(commandSplit[1]);

                        if (helpCommand == null || helpCommand == new Command())
                        {
                            ret = new DataEntities.ReturnResult.Result()
                            {
                                IsError = true,
                                ResultMessage = commandSplit[1] + " isn't a command",
                                ShowInUI = true,
                                Status = DataEntities.ReturnResult.Statuses.Success
                            };
                        }
                        else
                        {
                            ret = new DataEntities.ReturnResult.Result()
                            {
                                IsError = false,
                                ResultMessage = GetHelpCommand(helpCommand),
                                ShowInUI = true,
                                Status = DataEntities.ReturnResult.Statuses.Success
                            };
                        }
                    }
                    break;
                case "LOCATION":
                    ret = new DataEntities.ReturnResult.Result()
                    {
                        IsError = false,
                        ResultMessage = "You are in " + Game.Player.Location.Name,
                        ShowInUI = true,
                        Status = DataEntities.ReturnResult.Statuses.Success
                    };
                    break;
                case "LOOK":
                    if (Game.Player.Location.Destinations.Count() < 1)
                    {
                        ret = new DataEntities.ReturnResult.Result()
                        {
                            IsError = false,
                            ResultMessage = "You don't see shit",
                            ShowInUI = true,
                            Status = DataEntities.ReturnResult.Statuses.Success
                        };
                    }
                    else
                    {
                        ret = new DataEntities.ReturnResult.Result()
                        {
                            IsError = false,
                            ResultMessage = "You see:" + Environment.NewLine + string.Join(", " + Environment.NewLine, Game.Player.Location.Destinations.Select(x => x.Name)),
                            ShowInUI = true,
                            Status = DataEntities.ReturnResult.Statuses.Success
                        };
                    }
                    break;
                case "INV":
                    if (commandSplit.Length <= 1)
                    {
                        if (Game.Player.Inventory.InventorySlots.Count() < 1)
                        {
                            ret = new DataEntities.ReturnResult.Result()
                            {
                                IsError = false,
                                ResultMessage = "You don't have shit",
                                ShowInUI = true,
                                Status = DataEntities.ReturnResult.Statuses.Success
                            };
                        }
                        else
                        {
                            ret = new DataEntities.ReturnResult.Result()
                            {
                                IsError = false,
                                ResultMessage = "You have:" + Environment.NewLine + string.Join(", " + Environment.NewLine, Game.Player.Inventory.InventorySlots.Select(x => "(" + x.Amount + ") " + x.Item.Name)),
                                ShowInUI = true,
                                Status = DataEntities.ReturnResult.Statuses.Success
                            };
                        }
                    }
                    else
                    {
                        var itemNameSearch = commandSplit[1];

                        var s = Game.Player.Inventory.InventorySlots.Where(x => x.Item.Name.ToUpper() == itemNameSearch.ToUpper()).ToList();

                        if (s == null)
                        {
                            ret = new DataEntities.ReturnResult.Result()
                            {
                                IsError = false,
                                ResultMessage = "You don't have a " + itemNameSearch,
                                ShowInUI = true,
                                Status = DataEntities.ReturnResult.Statuses.NotValidCommand
                            };
                        }
                        else
                        {
                            var inventorySlot = s.FirstOrDefault();

                            var item = DataEntities.ItemList.GetItemByName(inventorySlot.Item.Name);

                            ret = new DataEntities.ReturnResult.Result()
                            {
                                IsError = false,
                                ResultMessage = item.Name + ": " + item.Description,
                                ShowInUI = true,
                                Status = DataEntities.ReturnResult.Statuses.Success
                            };
                        }
                    }
                    break;
                default:
                    ret = new DataEntities.ReturnResult.Result()
                    {
                        IsError = true,
                        ResultMessage = prefix + " isn't a command. Say help for a list of commands",
                        ShowInUI = true,
                        Status = DataEntities.ReturnResult.Statuses.Success
                    };
                    break;
            }

            return ret;
        }

        #region Classes

        public class Command
        {
            public string Name { get; set; }

            public List<string> Aliases { get; set; }

            public string Description { get; set; }

            public List<Parameter> Parameters { get; set; }
        }

        public class Parameter
        {
            public string Name
            {
                get; set;
            }

            public bool Optional
            {
                get; set;
            }
        }

        #endregion

    }
}
