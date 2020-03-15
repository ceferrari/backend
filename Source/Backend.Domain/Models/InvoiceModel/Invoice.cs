namespace Backend.Domain.Models.InvoiceModel
{
    public class Invoice
    {
        public Invoice(string xTeamControl, string currencyCode)
        {
            XTeamControl = xTeamControl;
            CurrencyCode = currencyCode;
        }

        public string XTeamControl { get; }
        public string CurrencyCode { get; }
    }
}