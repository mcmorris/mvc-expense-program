namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("BankImport")]
    public class BankImport : TrackedSelfValidatorEntity
    {
        [Key][Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int               Id          { get; set; }

        [Index("IDX_BankImportStatementId")]
        public int?               StatementId { get; set; }

        [Required]
        public int?               FileId      { get; set; }

        [Required][MaxLength(255)]
        public string            UserId      { get; set; }

        [Required]
        public int?              ImportStatusId { get; set; }

        [MaxLength(255)]
        public string            Bank        { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvalidTransaction> InvalidTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }

        #region Foreign Keys
        private Statement statement;
        private ImportStatus status;
        private File file;
        private User user;

        [Required]
        [ForeignKey("StatementId")]
        public virtual Statement Statement
        {
            get => this.statement;
            set
            {
                this.statement = value;
                this.StatementId = value?.Id;
            }
        }

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

        [Required]
        [ForeignKey("UserId")]
        public virtual User ImportedBy
        {
            get => this.user;
            set
            {
                this.user   = value;
                this.UserId = value?.Id;
            }
        }

        [Required][ForeignKey("ImportStatusId")]
        public virtual ImportStatus ImportStatus
        {
            get => this.status;
            set
            {
                this.status = value;
                this.ImportStatusId = value?.Id;
            }
        }
        #endregion

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
            this.Bank                = bank;
            this.InvalidTransactions = new HashSet<InvalidTransaction>();
            this.Transactions        = new HashSet<Transaction>();

            this.ImportedBy   = importedBy;
            this.ImportStatus = new ImportStatus(statusType);
            this.Statement    = statement;
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
            ICollection<TrackedChange> changes,
            bool                       active)
            : base(changes, active)
        {
            this.Id = id;
            this.File = file;
            this.Bank = bank;
            this.ImportedBy = importedBy;
            this.ImportStatus = importStatus;
            this.Statement = statement;
            this.Transactions = transactions;
            this.InvalidTransactions = invalidTransactions;
        }

        public void AddTransaction(Transaction newTransaction)
        {
            newTransaction.Statement = this.Statement;
            newTransaction.BankImport = this;

            if (this.Transactions.Contains(newTransaction)) { return; }
            this.Transactions.Add(newTransaction);
        }

        public void AddInvalidTransaction(InvalidTransaction newTransaction)
        {
            newTransaction.BankImport = this;
            this.InvalidTransactions.Add(newTransaction);
        }

        public void AddTransactions(ICollection<Transaction> newTransactions)
        {
            foreach (var newTransaction in newTransactions) { this.AddTransaction(newTransaction); }
        }

        public void AddInvalidTransactions(ICollection<InvalidTransaction> newTransactions)
        {
            foreach (var newTransaction in newTransactions) { this.AddInvalidTransaction(newTransaction); }
        }

        public void RemoveTransaction(Transaction transactionToDelete)
        {
            this.Transactions.Remove(transactionToDelete);
        }

        public void RemoveInvalidTransaction(InvalidTransaction transactionToDelete)
        {
            this.InvalidTransactions.Remove(transactionToDelete);
        }
    }
}