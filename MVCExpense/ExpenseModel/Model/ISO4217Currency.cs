namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Validation;

    [Table("ISO4217Currency")]
    public class ISO4217Currency : TrackedSelfValidatorEntity
    {
        [Key, Required, MaxLength(3)]
        public string        Id             { get; set; }

        [Required, Range(0, 4)]
        public int           Exponent       { get; set; }

        [Required, MaxLength(255), DataType(DataType.Text), Index("IDX_CurrencyName")]
        public string        Name           { get; set; }

        [DataType(DataType.DateTime), DateRangeBetweenYear2000AndNow]
        public DateTime?     WithdrawalDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ISO4217Currency()
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ISO4217Currency(string alphabeticCode, int exponent, string name, DateTime? withdrawalDate)
        {
            this.Id             = alphabeticCode;
            this.Exponent       = exponent;
            this.Name           = name;
            this.WithdrawalDate = withdrawalDate;
        }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ISO4217Currency(
            string    alphabeticCode,
            int       exponent,
            string    name,
            DateTime? withdrawalDate,
            ICollection<TrackedChange> changes,
            bool      active)
            : base(changes, active)
        {
            this.Id = alphabeticCode;
            this.Exponent = exponent;
            this.Name = name;
            this.WithdrawalDate = withdrawalDate;
        }
    }
}
