namespace MVCExpense
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CurrencySum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CurrencySum()
        {
            this.Accounts = new HashSet<Account>();
            this.BankImports = new HashSet<BankImport>();
            this.Statements = new HashSet<Statement>();
        }

        public int Id { get; set; }

        [StringLength(3)]
        public string InternalCurrencyCode { get; set; }

        public decimal? InternalDebitTotal { get; set; }

        public decimal? InternalCreditTotal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? InternalBalanceTotal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Accounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankImport> BankImports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Statement> Statements { get; set; }
    }
}
