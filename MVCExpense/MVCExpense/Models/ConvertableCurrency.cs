namespace MVCExpense
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ConvertableCurrency")]
    public partial class ConvertableCurrency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ConvertableCurrency()
        {
            this.DebitTransactions = new HashSet<Transaction>();
            this.CreditTransactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string Code { get; set; }

        public decimal Amount { get; set; }

        public DateTime Effective { get; set; }

        public decimal ConversionRate { get; set; }

        [Required]
        [StringLength(3)]
        public string InternalCurrency { get; set; }

        public decimal InternalAmount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> DebitTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> CreditTransactions { get; set; }
    }
}
