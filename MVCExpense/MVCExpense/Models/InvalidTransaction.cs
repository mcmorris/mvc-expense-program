namespace MVCExpense
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InvalidTransaction
    {
        public int Id { get; set; }

        public int ImportId { get; set; }

        [StringLength(255)]
        public string UserName { get; set; }

        [StringLength(255)]
        public string DateIncurred { get; set; }

        public string Description { get; set; }

        [StringLength(3)]
        public string DebitCurrencyCode { get; set; }

        [StringLength(255)]
        public string DebitValue { get; set; }

        [StringLength(3)]
        public string CreditCurrencyCode { get; set; }

        [StringLength(255)]
        public string CreditValue { get; set; }

        [StringLength(255)]
        public string Issue { get; set; }

        [StringLength(16)]
        public string MaskedCardNumber { get; set; }

        [StringLength(255)]
        public string CardExpiry { get; set; }

        public virtual BankImport BankImport { get; set; }
    }
}
