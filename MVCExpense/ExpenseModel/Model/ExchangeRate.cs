namespace ExpenseModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Validation;

    [Table("ExchangeRate")]
    public class ExchangeRate : SelfValidator
    {
        [Key][Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int             Id                     { get; set; }

        [Required][MaxLength(3)]
        public string          CurrencyFromId         { get; set; }

        [Required][MaxLength(3)]
        public string          CurrencyToId           { get; set; }

        [Required][DataType(DataType.DateTime)][DateRangeBetweenYear2000AndNow][Index("IDX_ExchangeRateEffective")]
        public DateTime        Effective              { get; set; }

        [Required]
        public decimal         ConversionRate         { get; set; }

        [Required]
        public bool            Active                 { get; set; }

        [Required][ForeignKey("CurrencyFromId")][Index("IDX_ExchangeRateCurrencyFromId")]
        public virtual ISO4217Currency CurrencyFrom
        {
            get => this.CurrencyFrom;
            set
            {
                this.CurrencyFrom = value;
                this.CurrencyFromId = value?.Id;
            }
        }

        [Required][ForeignKey("CurrencyToId")]
        public virtual ISO4217Currency CurrencyTo
        {
            get => this.CurrencyTo;
            set
            {
                this.CurrencyTo = value;
                this.CurrencyToId = value?.Id;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExchangeRate()
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExchangeRate(DateTime effective, decimal conversionRate, ISO4217Currency currencyFrom, ISO4217Currency currencyTo, bool active=true)
        {
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

        public decimal Convert(decimal externalAmount)
        {
            return externalAmount * this.ConversionRate;
        }
    }
}
