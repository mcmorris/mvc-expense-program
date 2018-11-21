﻿namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using global::Validation;

    [Table("Statement")]
    public class Statement : TrackedEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int          Id           { get; set; }

        public int? ImportStatusId        { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateRangeBetweenYear2000AndNow]
        public DateTime     Month        { get; set; }

        [ForeignKey("ImportStatusId")]
        public virtual ImportStatus ImportStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankImport> BankImports { get; set; }

        #region Calculated fields
        [NotMapped]
        public DateTime CoversFrom             => this.BankImports.Min(i => i.CoversFrom);

        [NotMapped]
        public DateTime CoversUntil            => this.BankImports.Max(i => i.CoversUntil);

        [NotMapped]
        public int ImportCount                 => this.BankImports.Count;

        [NotMapped]
        public int SuccessfulTransactionsCount => this.BankImports.Sum(i => i.SuccessfulTransactionsCount);

        [NotMapped]
        public int TransactionErrorsCount      => this.BankImports.Sum(i => i.TransactionErrorsCount);

        [NotMapped]
        public decimal DebitSum  => this.BankImports.Sum(i => i.DebitSum);

        [NotMapped]
        public decimal CreditSum => this.BankImports.Sum(i => i.CreditSum);

        [NotMapped]
        public decimal Balance   => this.DebitSum - this.CreditSum;
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Statement()
        {
            this.BankImports = new HashSet<BankImport>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Statement(
            int                     id,
            DateTime                month,
            ImportStatus            importStatus,
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
            this.ImportStatusId = this.ImportStatus.Id;
            this.BankImports = bankImports;
        }
    }
}