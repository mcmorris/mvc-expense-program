namespace ExpenseModel
{
    using System;

    public class Money
    {
        public int             Id             { get; set; }
        public ExchangeRate    ExchangeRate   { get; set; }
        public DateTime        IncurredOn     { get; set; }
        public decimal         ExternalAmount { get; set; }
        public decimal         InternalAmount { get; set; }

        public Money(int id, 
                     ExchangeRate exchangeRate, 
                     DateTime incurredOn, 
                     decimal externalAmount, 
                     decimal internalAmount)
        {
            this.Id = id;
            this.ExchangeRate = exchangeRate;
            this.IncurredOn = incurredOn;
            this.ExternalAmount = externalAmount;
            this.InternalAmount = internalAmount;
        }
    }
}