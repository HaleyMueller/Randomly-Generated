using System;
using System.Collections.Generic;
using System.Drawing;

namespace DataEntities
{
    public class ReturnResult : List<DataEntities.ReturnResult.Result>
    {
        public Statuses Status { get; set; }

        public string StatusMessage { get; set; }

        public bool IsError { get; set; } = false;

        public new void Add(Result r)
        {
            if (r.IsError == true)
            {
                this.IsError = true;
            }

            base.Add(r);
        }

        public class Result
        {
            public Statuses Status { get; set; }

            public string ResultMessage { get; set; }

            public bool IsError { get; set; } = false;

            public bool ShowInUI { get; set; } = true;

            public Color Color
            {
                get
                {
                    if (Status == Statuses.NotValidCommand)
                    {
                        return Color.Yellow;
                    }
                    else if (Status == Statuses.Other)
                    {
                        return Color.DarkSlateGray;
                    }
                    else if (IsError)
                    {
                        return Color.DarkRed;
                    }
                    else
                    {
                        return Color.Black;
                    }
                }
            }
        }

        public enum Statuses
        {
            Success,
            NotValidCommand,
            Other,
        }
    }
}
