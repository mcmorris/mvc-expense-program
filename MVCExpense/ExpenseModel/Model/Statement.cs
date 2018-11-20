namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Statement : TrackedEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Statement()
        {
            this.BankImports = new HashSet<BankImport>();
        }

        public Statement(
            int                     id,
            DateTime                month,
            ImportStatus            importStatus,
            DateTime                coversFrom,
            DateTime                coversUntil,
            ICollection<BankImport> bankImports,
            DateTime                created,
            string                  createdBy,
            DateTime?               modified,
            string                  modifiedBy,
            DateTime?               inactiveSince,
            bool                    active)
            : base(created, createdBy, modified, modifiedBy, inactiveSince, active)
        {
            this.Id = id;
            this.Month = month;
            this.ImportStatus = importStatus;
            this.CoversFrom = coversFrom;
            this.CoversUntil = coversUntil;
            this.BankImports = bankImports;
        }


        public int             Id                          { get; set; }
        public DateTime        Month                       { get; set; }
        public ImportStatus    ImportStatus                { get; set; }
        public DateTime        CoversFrom                  { get; set; }
        public DateTime        CoversUntil                 { get; set; }

        // Calculated fields
        public int ImportCount => this.BankImports.Count;
        public int SuccessfulTransactionsCount => this.BankImports.Sum(i => i.SuccessfulTransactionsCount);
        public int TransactionErrorsCount => this.BankImports.Sum(i => i.TransactionErrorsCount);

        public decimal DebitSum => this.BankImports.Sum(i => i.DebitSum);
        public decimal CreditSum => this.BankImports.Sum(i => i.CreditSum);
        public decimal Balance => this.DebitSum - this.CreditSum;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankImport> BankImports { get; set; }
    }
}