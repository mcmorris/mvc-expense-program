namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BankImport : TrackedEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankImport()
        {
            this.InvalidTransactions = new HashSet<InvalidTransaction>();
            this.Transactions        = new HashSet<Transaction>();
        }

        public BankImport(
            int                      id,
            Statement                statement,
            File                     file,
            User                     importedBy,
            string                   bank,
            DateTime                 coversFrom,
            DateTime                 coversUntil,
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
            this.File = file;
            this.ImportedBy = importedBy;
            this.Bank = bank;
            this.CoversFrom = coversFrom;
            this.CoversUntil = coversUntil;
            this.ImportStatus = importStatus;
            this.Transactions = transactions;
            this.InvalidTransactions = invalidTransactions;
        }

        public int Id                    { get; set; }
        public Statement Statement       { get; set; }
        public File File                 { get; set; }
        public User ImportedBy           { get; set; }
        public string Bank               { get; set; }
        public DateTime CoversFrom       { get; set; }
        public DateTime CoversUntil      { get; set; }

        
        public ImportStatus ImportStatus { get; set; }

        // Calculated fields
        public int SuccessfulTransactionsCount => this.Transactions.Count;

        public int TransactionErrorsCount => this.InvalidTransactions.Count;

        public decimal TotalTransactionsCount => this.SuccessfulTransactionsCount + this.TransactionErrorsCount;

        public decimal DebitSum => this.Transactions.Sum(t => t.Debit.InternalAmount);

        public decimal CreditSum => this.Transactions.Sum(t => t.Credit.InternalAmount);

        public decimal Balance => this.DebitSum - this.CreditSum;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<InvalidTransaction> InvalidTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Transaction> Transactions { get; set; }
    }
}