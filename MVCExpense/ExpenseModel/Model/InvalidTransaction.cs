namespace ExpenseModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InvalidTransaction")]
    public class InvalidTransaction
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int    Id                 { get; set; }

        [Required]
        [Index("IDX_InvalidTransactionBankImportId")]
        public int    BankImportId       { get; set; }

        [ForeignKey("BankImportId")]
        public virtual BankImport BankImport     { get; set; }

        [MaxLength(255)]
        [DataType(DataType.Text)]
        public string UserName           { get; set; }

        [MaxLength(255)]
        [DataType(DataType.DateTime)]
        public string DateIncurred       { get; set; }

        [DataType(DataType.Text)]
        public string Description        { get; set; }

        [MaxLength(3)]
        public string DebitCurrencyCode  { get; set; }

        [MaxLength(255)]
        [DataType(DataType.Currency)]
        public string DebitValue         { get; set; }

        [MaxLength(3)]
        public string CreditCurrencyCode { get; set; }

        [MaxLength(255)]
        [DataType(DataType.Currency)]
        public string CreditValue        { get; set; }

        [MaxLength(255)]
        [DataType(DataType.Text)]
        public string Issue              { get; set; }

        [MaxLength(16)]
        [DataType(DataType.CreditCard)]
        public string MaskedCardNumber   { get; set; }

        [MaxLength(255)]
        [DataType(DataType.DateTime)]
        public string CardExpiry         { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvalidTransaction()
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvalidTransaction(
            int        id,
            BankImport bankImport,
            string     userName,
            string     dateIncurred,
            string     description,
            string     debitCurrencyCode,
            string     debitValue,
            string     creditCurrencyCode,
            string     creditValue,
            string     issue,
            string     maskedCardNumber,
            string     cardExpiry)
        {
            this.Id = id;
            this.BankImport = bankImport;
            this.BankImportId = this.BankImport.Id;
            this.UserName = userName;
            this.DateIncurred = dateIncurred;
            this.Description = description;
            this.DebitCurrencyCode = debitCurrencyCode;
            this.DebitValue = debitValue;
            this.CreditCurrencyCode = creditCurrencyCode;
            this.CreditValue = creditValue;
            this.Issue = issue;
            this.MaskedCardNumber = maskedCardNumber;
            this.CardExpiry = cardExpiry;
        }

        public InvalidTransaction(string[] parsedAndSanitizedInput, string parsingIssue)
        {
            // TODO: 
        }
    }
}