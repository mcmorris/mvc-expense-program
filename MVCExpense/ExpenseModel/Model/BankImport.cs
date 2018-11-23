namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("BankImport")]
    public class BankImport : TrackedEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int               Id          { get; set; }

        [Index("IDX_BankImportStatementId")]
        public int?               StatementId { get; set; }

        [Required]
        public int               FileId      { get; set; }

        [Required]
        [MaxLength(255)]
        public string            UserId      { get; set; }

        [Required]
        public int               ImportStatusId { get; set; }

        [ForeignKey("StatementId")]
        public virtual Statement Statement   { get; set; }

        [ForeignKey("FileId")]
        public virtual File      File        { get; set; }

        [ForeignKey("UserId")]
        public virtual User      ImportedBy  { get; set; }

        [MaxLength(255)]
        public string            Bank        { get; set; }

        [ForeignKey("ImportStatusId")]
        public virtual ImportStatus ImportStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvalidTransaction> InvalidTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }

        #region Calculated fields
        [NotMapped]
        public DateTime CoversFrom  => this.Transactions.Min(t => t.DateIncurred);

        [NotMapped]
        public DateTime CoversUntil => this.Transactions.Max(t => t.DateIncurred);

        [NotMapped]
        public int SuccessfulTransactionsCount => this.Transactions.Count;

        [NotMapped]
        public int TransactionErrorsCount => this.InvalidTransactions.Count;

        [NotMapped]
        public decimal TotalTransactionsCount => this.SuccessfulTransactionsCount + this.TransactionErrorsCount;

        [NotMapped]
        public decimal DebitSum => this.Transactions.Sum(t => t.Debit.InternalAmount);

        [NotMapped]
        public decimal CreditSum => this.Transactions.Sum(t => t.Credit.InternalAmount);

        [NotMapped]
        public decimal Balance => this.DebitSum - this.CreditSum;
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankImport()
        {
            this.InvalidTransactions = new HashSet<InvalidTransaction>();
            this.Transactions        = new HashSet<Transaction>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankImport(Statement statement, User importedBy, string bank, StatusTypes statusType)
        {
            this.Statement           = statement;
            this.StatementId         = this.Statement.Id;
            this.ImportedBy          = importedBy;
            this.UserId              = this.ImportedBy.Id;
            this.Bank                = bank;
            this.ImportStatus        = new ImportStatus(statusType);
            this.ImportStatusId      = this.ImportStatus.Id;
            this.InvalidTransactions = new HashSet<InvalidTransaction>();
            this.Transactions        = new HashSet<Transaction>();
        }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankImport(
            int                      id,
            Statement                statement,
            File                     file,
            User                     importedBy,
            string                   bank,
            ImportStatus             importStatus,
            ICollection<Transaction> transactions,
            ICollection<InvalidTransaction> invalidTransactions,
            DateTime                 created,
            string                   createdBy,
            DateTime?                modified,
            string                   modifiedBy,
            DateTime?                inactiveSince,
            bool                     active)
            : base(created, createdBy, modified, modifiedBy, inactiveSince, active)
        {
            this.Id = id;
            this.Statement = statement;
            this.StatementId = this.Statement.Id;
            this.File = file;
            this.FileId = this.File.Id;
            this.ImportedBy = importedBy;
            this.UserId = this.ImportedBy.Id;
            this.Bank = bank;
            this.ImportStatus = importStatus;
            this.ImportStatusId = this.ImportStatus.Id;
            this.Transactions = transactions;
            this.InvalidTransactions = invalidTransactions;
        }
    }
}