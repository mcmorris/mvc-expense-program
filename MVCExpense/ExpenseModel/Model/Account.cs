namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using global::Validation;

    [Table("Account")]
    public class Account : TrackedSelfValidatorEntity
    {
        [Required, MaxLength(255), Key, Column(Order = 0)]
        public string UserId             { get; set; }

        [Required, StringLength(16), Key, Column(Order = 1)]
        public string MaskedCardNumber   { get; set; }

        [ForeignKey("UserId")]
        public virtual User     User     { get; set; }

        [Required, Index("IDX_CCExpiry"), DataType(DataType.DateTime), DateAfter200011]
        public DateTime Expiry           { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }

        #region Calculated fields
        [NotMapped]
        public decimal DebitSum => this.Transactions.Sum(t => t.Debit.InternalAmount);

        [NotMapped]
        public decimal CreditSum => this.Transactions.Sum(t => t.Credit.InternalAmount);

        [NotMapped]
        public decimal Balance => this.DebitSum - this.CreditSum;
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            this.Transactions = new HashSet<Transaction>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account(User user, string maskedCardNumber, int expiryYear, int expiryMonth)
        {
            this.User             = user;
            this.UserId           = user?.Id;
            this.MaskedCardNumber = maskedCardNumber;
            this.Expiry           = new DateTime(expiryYear, expiryMonth, 1);
            this.Transactions     = new HashSet<Transaction>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account(User user, string maskedCardNumber, DateTime expiry, ICollection<Transaction> transactions, ICollection<TrackedChange> changes, bool active)
            : base(changes, active)
        {
            this.User = user;
            this.UserId = user?.Id;
            this.MaskedCardNumber = maskedCardNumber;
            this.Expiry = expiry;
            this.Transactions = transactions;
        }
    }
}