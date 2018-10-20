namespace MVCExpense
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Statement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Statement()
        {
            this.AttachmentCoverages = new HashSet<AttachmentCoverage>();
            this.BankImports = new HashSet<BankImport>();
            this.Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Month { get; set; }

        public int StatusId { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentCoverage> AttachmentCoverages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankImport> BankImports { get; set; }

        public virtual CurrencySum CurrencySum { get; set; }

        public virtual ImportStatus ImportStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
