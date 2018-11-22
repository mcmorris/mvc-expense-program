namespace ExpenseModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Validation;

    [Table("ISO4217Currency")]
    public class ISO4217Currency : TrackedEntity
    {
        [Key]
        [Required]
        [MaxLength(3)]
        public string        Id             { get; set; }

        [Required]
        [Range(0, 4)]
        public int           Exponent       { get; set; }

        [Required]
        [MaxLength(255)]
        [DataType(DataType.Text)]
        [Index("IDX_CurrencyName")]
        public string        Name           { get; set; }

        [DataType(DataType.DateTime)]
        [DateRangeBetweenYear2000AndNow]
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
            DateTime  created,
            string    createdBy,
            DateTime? modified,
            string    modifiedBy,
            DateTime? inactiveSince,
            bool      active)
            : base(created, createdBy, modified, modifiedBy, inactiveSince, active)
        {
            this.Id = alphabeticCode;
            this.Exponent = exponent;
            this.Name = name;
            this.WithdrawalDate = withdrawalDate;
        }
    }
}
