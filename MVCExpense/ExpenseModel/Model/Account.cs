namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("Account")]
    public class Account : TrackedEntity
    {
        [Required]
        [MaxLength(255)]
        [Key, Column(Order = 0)]
        public string UserId             { get; set; }

        [Required]
        [StringLength(16)]
        [Key, Column(Order = 1)]       
        [DataType(DataType.CreditCard)]
        public string MaskedCardNumber   { get; set; }

        [ForeignKey("UserId")]
        public virtual User     User     { get; set; }

        [Required]
        [Index("IDX_CCExpiry")]
        [DataType(DataType.DateTime)]
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
        public Account(User user, string maskedCardNumber, DateTime expiry)
        {
            this.User             = user;
            this.UserId           = user?.Id;
            this.MaskedCardNumber = maskedCardNumber;
            this.Expiry           = expiry;
            this.Transactions     = new HashSet<Transaction>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account(User user, string maskedCardNumber, DateTime expiry, ICollection<Transaction> transactions, DateTime created, string createdBy, DateTime? modified, string modifiedBy, DateTime? inactiveSince, bool active)
            : base(created, createdBy, modified, modifiedBy, inactiveSince, active)
        {
            this.User = user;
            this.UserId = user?.Id;
            this.MaskedCardNumber = maskedCardNumber;
            this.Expiry = expiry;
            this.Transactions = transactions;
        }
    }
}