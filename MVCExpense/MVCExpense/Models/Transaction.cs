namespace MVCExpense
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transaction()
        {
            this.AttachmentCoverages = new HashSet<AttachmentCoverage>();
        }

        public int Id { get; set; }

        public int StatementId { get; set; }

        public int BankImportId { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        public DateTime DateIncurred { get; set; }

        public string Description { get; set; }

        public int? DebitCurrency { get; set; }

        public int? CreditCurrency { get; set; }

        [Required]
        [StringLength(16)]
        public string MaskedCardNumber { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttachmentCoverage> AttachmentCoverages { get; set; }

        public virtual BankImport BankImport { get; set; }

        public virtual ConvertableCurrency ConvertableCurrency { get; set; }

        public virtual ConvertableCurrency ConvertableCurrency1 { get; set; }

        public virtual Statement Statement { get; set; }
    }
}
