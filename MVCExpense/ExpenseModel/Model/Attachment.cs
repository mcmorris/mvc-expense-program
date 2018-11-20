namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;

    public class Attachment : TrackedEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attachment()
        {
            this.TransactionsCovered = new HashSet<Transaction>();
        }

        public Attachment(
            int       id,
            File      file,
            string    issue,
            ImportStatus status,
            Statement statementCovered,
            ICollection<Transaction> transactionsCovered,
            DateTime  created,
            string    createdBy,
            DateTime? modified,
            string    modifiedBy,
            DateTime? inactiveSince,
            bool      active)
            : base(created, createdBy, modified, modifiedBy, inactiveSince, active)
        {
            this.Id = id;
            this.File = file;
            this.Issue = issue;
            this.Status = status;
            this.StatementCovered = statementCovered;
            this.TransactionsCovered = transactionsCovered;
        }

        public int             Id               { get; set; }
        public File            File             { get; set; }
        public string          Issue            { get; set; }
        public ImportStatus    Status           { get; set; }
        public Statement       StatementCovered { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> TransactionsCovered { get; set; }
    }
}