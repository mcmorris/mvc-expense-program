namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Account : TrackedEntity
    {
        public User     User             { get; set; }
        public string   MaskedCardNumber { get; set; }
        public DateTime Expiry           { get; set; }

        // Calculated fields
        public decimal DebitSum => this.Transactions.Sum(t => t.Debit.InternalAmount);

        public decimal CreditSum => this.Transactions.Sum(t => t.Credit.InternalAmount);

        public decimal Balance => this.DebitSum - this.CreditSum;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Transaction> Transactions { get; set; }

        public Account(User user, string maskedCardNumber, DateTime expiry, DateTime created, string createdBy, DateTime? modified, string modifiedBy, DateTime? inactiveSince, bool active)
            : base(created, createdBy, modified, modifiedBy, inactiveSince, active)
        {
            this.User = user;
            this.MaskedCardNumber = maskedCardNumber;
            this.Expiry = expiry;
        }
    }
}