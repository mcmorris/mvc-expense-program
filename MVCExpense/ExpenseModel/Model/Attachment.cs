namespace ExpenseModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Attachment")]
    public class Attachment : TrackedSelfValidatorEntity
    {
        [Key][Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int          Id                 { get; set; }

        [Required][Index("IDX_AttachmentFileId")]
        public int?         FileId             { get; set; }

        [Required][Index("IDX_AttachmentStatementId")]
        public int?         StatementId        { get; set; }

        [Required]
        public int?         ImportStatusId     { get; set; }

        [MaxLength(255)]
        public string       Issue              { get; set; }

        #region Foreign Keys
        private File file;
        private ImportStatus status;
        private Statement statement;

        [Required]
        [ForeignKey("FileId")]
        public virtual File File
        {
            get => this.file;
            set
            {
                this.file = value;
                this.FileId = value?.Id;
            }
        }

        [Required][ForeignKey("StatusId")]
        public virtual ImportStatus Status
        {
            get => this.status;
            set
            {
                this.status = value;
                this.ImportStatusId = value?.Id;
            }
        }

        [Required][ForeignKey("StatementId")]
        public virtual Statement StatementCovered
        {
            get => this.statement;
            set
            {
                this.statement = value;
                this.StatementId = value?.Id;
            }
        }
        #endregion

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
            this.Issue = issue;
            this.Status = status;
            this.StatementCovered = statementCovered;
            this.TransactionsCovered = transactionsCovered;
        }

        public void AddTransaction(Transaction transactionCovered)
        {
            if (this.TransactionsCovered == null) { this.TransactionsCovered = new List<Transaction>(); }
            if (this.TransactionsCovered.Contains(transactionCovered)) { return; }
            this.TransactionsCovered.Add(transactionCovered);
        }

        public bool CoversTransaction(Transaction transactionToCheck)
        {
            return this.TransactionsCovered.Contains(transactionToCheck);
        }

        public void AddTransactions(ICollection<Transaction> newTransactions)
        {
            foreach (var newTransaction in newTransactions) { this.AddTransaction(newTransaction); }
        }

        public void RemoveCoverage(Transaction transactionToDelete)
        {
            this.TransactionsCovered.Remove(transactionToDelete);
        }
    }
}