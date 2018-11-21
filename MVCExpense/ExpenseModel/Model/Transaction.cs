namespace ExpenseModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Validation;

    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id                          { get; set; }

        [Required]
        [Index("IDX_TransactionStatementId")]
        public int StatementId                 { get; set; }

        [Required]
        [Index("IDX_TransactionBankImportId")]
        public int BankImportId                { get; set; }

        public int? DebitId                     { get; set; }

        public int? CreditId                    { get; set; }

        [ForeignKey("StatementId")]
        public virtual Statement Statement     { get; set; }

        [ForeignKey("BankImportId")]
        public virtual BankImport BankImport   { get; set; }

        [Required]
        [MaxLength(255)]
        [DataType(DataType.Text)]
        public string UserName                 { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DateRangeBetweenYear2000AndNow]
        public DateTime DateIncurred           { get; set; }

        [DataType(DataType.Text)]
        public string Description              { get; set; }

        [ForeignKey("DebitId")]
        public virtual Money Debit             { get; set; }

        [ForeignKey("CreditId")]
        public virtual Money Credit            { get; set; }

        [Required]
        [StringLength(16)]
        [DataType(DataType.CreditCard)]
        public string MaskedCardNumber         { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transaction()
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transaction(
            int        id,
            Statement  statement,
            BankImport bankImport,
            string     userName,
            DateTime   dateIncurred,
            string     description,
            Money      debit,
            Money      credit,
            string     maskedCCNumber)
        {
            this.Id = id;
            this.Statement = statement;
            this.StatementId = this.Statement.Id;
            this.BankImport = bankImport;
            this.BankImportId = this.BankImport.Id;
            this.UserName = userName;
            this.DateIncurred = dateIncurred;
            this.Description = description;
            this.Debit = debit;
            this.Credit = credit;
            this.MaskedCardNumber = maskedCCNumber;
        }

        public Transaction(string[] parsedAndSanitizedInput)
        {
            // TODO: 
        }

        public static string IsValid(string[] inputFields)
        {
            // TODO:
            return null;
        }
    }
}
