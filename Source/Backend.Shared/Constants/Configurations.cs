using System.Globalization;

namespace Backend.Shared.Constants
{
    public static class Configurations
    {
        public static CultureInfo CultureInfo = new CultureInfo("pt-BR")
        {
            NumberFormat =
            {
                NumberGroupSeparator = ".",
                NumberDecimalSeparator = ",",
                CurrencyGroupSeparator = ".",
                CurrencyDecimalSeparator = ","
            }
        };
    }
}
