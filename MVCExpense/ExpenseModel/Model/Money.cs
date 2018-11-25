namespace ExpenseModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Validation;

    [Table("Money")]
    public class Money : SelfValidator
    {
        [Key][Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int             Id             { get; set; }

        [Required]
        public int?            ExchangeRateId { get; set; }

        [Required][DataType(DataType.DateTime)][DateRangeBetweenYear2000AndNow]
        public DateTime        IncurredOn     { get; set; }

        [Required][DataType(DataType.Currency)]
        public decimal         ExternalAmount { get; set; }

        [Required][DataType(DataType.Currency)][Index("IDX_MoneyInternalAmount")]
        public decimal         InternalAmount { get; set; }


        [Required]
        [ForeignKey("ExchangeRateId")]
        public virtual ExchangeRate ExchangeRate
        {
            get => this.ExchangeRate;
            set
            {
                this.ExchangeRate = value;
                this.ExchangeRateId = value?.Id;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Money()
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Money(ExchangeRate exchangeRate, 
                     DateTime incurredOn, 
                     decimal externalAmount, 
                     decimal internalAmount)
        {
            this.ExchangeRate = exchangeRate;
            this.IncurredOn = incurredOn;
            this.ExternalAmount = externalAmount;
            this.InternalAmount = internalAmount;

            if (exchangeRate != null) { this.ExchangeRateId = exchangeRate.Id; }
        }
    }
}