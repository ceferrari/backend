using System.Collections.Generic;

namespace Backend.Shared.Constants
{
    public static class DomainValues
    {
        public static class AcceptedCurrencies
        {
            public const string Brl                 = "BRL";
            public const string Usd                 = "USD";
            public const string Eur                 = "EUR";
            public static readonly List<string> List = new List<string> { Brl, Usd, Eur };
        }

        public static class Cart
        {
            public static class Status
            {
                public const string Pending         = "PENDING";
                public const string Cancel          = "CANCEL";
                public const string Done            = "DONE";
                public static readonly List<string> List = new List<string> { Pending, Cancel, Done };
            }
        }
    }
}
