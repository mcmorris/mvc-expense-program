namespace ExpenseModel
{
    using System;

    public class ExchangeRate
    {
        public int             Id             { get; set; }
        public DateTime        Effective      { get; set; }
        public decimal         ConversionRate { get; set; }
        public ISO4217Currency CurrencyFrom   { get; set; }
        public ISO4217Currency CurrencyTo     { get; set; }
        public bool            Active         { get; set; }

        public ExchangeRate(
            int             id,
            DateTime        effective,
            decimal         conversionRate,
            ISO4217Currency currencyFrom,
            ISO4217Currency currencyTo,
            bool            active)
        {
            this.Id = id;
            this.Effective = effective;
            this.ConversionRate = conversionRate;
            this.CurrencyFrom = currencyFrom;
            this.CurrencyTo = currencyTo;
            this.Active = active;
        }

        public override bool Equals(object other)
        {
            if (this.GetType() != other?.GetType()) { return false; }

            var otherRate = other as ExchangeRate;
            if (this.CurrencyFrom != otherRate?.CurrencyFrom || this.CurrencyTo != otherRate?.CurrencyTo) { return false; }
            return this.ConversionRate == otherRate?.ConversionRate;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (17 * this.CurrencyFrom.GetHashCode() * this.CurrencyTo.GetHashCode()) ^ this.ConversionRate.GetHashCode();
            }
        }
    }
}
