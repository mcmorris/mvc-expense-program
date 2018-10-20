namespace MVCExpense
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BankImport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankImport()
        {
            this.InvalidTransactions = new HashSet<InvalidTransaction>();
            this.Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }

        public int StatementId { get; set; }

        public int ImportFileId { get; set; }

        [Required]
        [StringLength(255)]
        public string ImportedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime Month { get; set; }

        [Required]
        [StringLength(255)]
        public string Bank { get; set; }

        public int StatusId { get; set; }

        public int TotalTransactions { get; set; }

        public int CurrencySumId { get; set; }

        public DateTime CoversFrom { get; set; }

        public DateTime CoversUntil { get; set; }

        public DateTime FirstCreated { get; set; }

        public DateTime LastModified { get; set; }

        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(255)]
        public string LastModifiedBy { get; set; }

        public virtual CurrencySum CurrencySum { get; set; }

        public virtual File File { get; set; }

        public virtual User User { get; set; }

        public virtual Statement Statement { get; set; }

        public virtual ImportStatus ImportStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvalidTransaction> InvalidTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
