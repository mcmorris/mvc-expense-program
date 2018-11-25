namespace ExpenseModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("InvalidTransaction")]
    public class InvalidTransaction : SelfValidator
    {
        [Key][Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int    Id                 { get; set; }

        [Required][Index("IDX_InvalidTransactionBankImportId")]
        public int?   BankImportId       { get; set; }

        [MaxLength(255)][DataType(DataType.Text)]
        public string UserName           { get; set; }

        [MaxLength(255)][DataType(DataType.DateTime)]
        public string DateIncurred       { get; set; }

        [DataType(DataType.Text)]
        public string Description        { get; set; }

        [MaxLength(3)]
        public string DebitCurrencyCode  { get; set; }

        [MaxLength(255)][DataType(DataType.Currency)]
        public string DebitValue         { get; set; }

        [MaxLength(3)]
        public string CreditCurrencyCode { get; set; }

        [MaxLength(255)][DataType(DataType.Currency)]
        public string CreditValue        { get; set; }

        [MaxLength(255)][DataType(DataType.Text)]
        public string Issue              { get; set; }

        [MaxLength(16)][DataType(DataType.CreditCard)]
        public string MaskedCardNumber   { get; set; }

        [MaxLength(255)][DataType(DataType.DateTime)]
        public string CardExpiry         { get; set; }

        [ForeignKey("BankImportId")]
        public virtual BankImport BankImport
        {
            get => this.BankImport;
            set
            {
                this.BankImport = value;
                this.BankImportId = value?.Id;
            }
        }

[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvalidTransaction()
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvalidTransaction(
            BankImport bankImport,
            string     userName,
            string     dateIncurred,
            string     description,
            string     debitCurrencyCode,
            string     debitValue,
            string     creditCurrencyCode,
            string     creditValue,
            ValidationResult issue,
            string     maskedCardNumber,
            string     cardExpiry)
        {
            this.BankImport = bankImport;
            this.UserName = userName;
            this.DateIncurred = dateIncurred;
            this.Description = description;
            this.DebitCurrencyCode = debitCurrencyCode;
            this.DebitValue = debitValue;
            this.CreditCurrencyCode = creditCurrencyCode;
            this.CreditValue = creditValue;
            this.Issue = issue.ErrorMessage;
            this.MaskedCardNumber = maskedCardNumber;
            this.CardExpiry = cardExpiry;
        }

        public InvalidTransaction(string[] parsedAndSanitizedInput, ValidationResult parsingIssue)
        {
            // TODO: 
        }
    }
}