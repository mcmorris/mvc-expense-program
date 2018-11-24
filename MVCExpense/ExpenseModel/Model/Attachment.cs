namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Attachment")]
    public class Attachment : TrackedSelfValidatorEntity
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int          Id                 { get; set; }

        [Required, Index("IDX_AttachmentFileId")]
        public int          FileId             { get; set; }

        [Required, Index("IDX_AttachmentStatementId")]
        public int          StatementId        { get; set; }

        [Required]
        public int          StatusId           { get; set; }

        [ForeignKey("FileId")]
        public virtual File File               { get; set; }

        [MaxLength(255)]
        public string       Issue              { get; set; }

        [ForeignKey("StatusId")]
        public virtual     ImportStatus Status { get; set; }

        [ForeignKey("StatementId")]
        public virtual Statement StatementCovered { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> TransactionsCovered { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attachment()
        {
            this.TransactionsCovered = new HashSet<Transaction>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attachment(File file, string issue, StatusTypes status, Statement statementCovered, ICollection<Transaction> transactionsCovered)
        {
            this.File                = file;
            this.FileId              = this.File.Id;
            this.Issue               = issue;
            this.Status              = new ImportStatus(status);
            this.StatementCovered    = statementCovered;
            this.TransactionsCovered = transactionsCovered;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attachment(
            int       id,
            File      file,
            string    issue,
            ImportStatus status,
            Statement statementCovered,
            ICollection<Transaction> transactionsCovered,
            ICollection<TrackedChange> changes,
            bool                       active)
            : base(changes, active)
        {
            this.Id = id;
            this.File = file;
            this.FileId = this.File.Id;
            this.Issue = issue;
            this.Status = status;
            this.StatementCovered = statementCovered;
            this.TransactionsCovered = transactionsCovered;
        }
    }
}