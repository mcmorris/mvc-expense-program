namespace MVCExpense
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            this.Transactions = new HashSet<Transaction>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string UserName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string MaskedCardNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime Expiry { get; set; }

        public int CurrencySumId { get; set; }

        public DateTime FirstCreated { get; set; }

        public DateTime LastModified { get; set; }

        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(255)]
        public string LastModifiedBy { get; set; }

        public bool Active { get; set; }

        public virtual CurrencySum CurrencySum { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
