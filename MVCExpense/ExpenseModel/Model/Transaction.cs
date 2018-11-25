namespace ExpenseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ExpenseModel.Validation;

    using global::Validation;

    [Table("Transaction")]
    public class Transaction : SelfValidator
    {
        [Key][Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id                          { get; set; }

        [Required][Index("IDX_TransactionStatementId")]
        public int? StatementId                { get; set; }

        [Required][Index("IDX_TransactionBankImportId")]
        public int? BankImportId               { get; set; }

        public int? DebitId                    { get; set; }

        public int? CreditId                   { get; set; }

        [Required][MaxLength(255)][DataType(DataType.Text)]
        public string UserName                 { get; set; }

        [Required][DataType(DataType.DateTime)][DateRangeBetweenYear2000AndNow]
        public DateTime DateIncurred           { get; set; }

        [DataType(DataType.Text)]
        public string Description              { get; set; }

        [Required][StringLength(16)][DataType(DataType.CreditCard)]
        public string MaskedCardNumber         { get; set; }

        [ForeignKey("StatementId")]
        public virtual Statement Statement
        {
            get => this.Statement;
            set
            {
                this.Statement   = value;
                this.StatementId = value?.Id;
            }
        }

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

        [ForeignKey("DebitId")]
        public virtual Money Debit
        {
            get => this.Debit;
            set
            {
                this.Debit = value;
                this.DebitId = value?.Id;
            }
        }

        [ForeignKey("CreditId")]
        public virtual Money Credit
        {
            get => this.Credit;
            set
            {
                this.Credit = value;
                this.CreditId = value?.Id;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transaction()
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transaction(string userName, DateTime dateIncurred, string description, ExchangeRate debitExchangeRate, decimal externalDebitAmount, ExchangeRate creditExchangeRate, decimal externalCreditAmount, string maskedCCNumber)
        {
            this.UserName         = userName;
            this.DateIncurred     = dateIncurred;
            this.Description      = description;
            this.Debit            = new Money(debitExchangeRate, this.DateIncurred, externalDebitAmount, debitExchangeRate.Convert(externalDebitAmount));
            this.Credit           = new Money(creditExchangeRate, this.DateIncurred, externalCreditAmount, creditExchangeRate.Convert(externalCreditAmount));
            this.MaskedCardNumber = maskedCCNumber;
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
            this.BankImport = bankImport;
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

        // Format: DateIncurred, Description, [DebitCode, DebitAmount], [DebitCode, DebitAmount], MaskedCardNumber
        public static IEnumerable<ValidationResult> WillBeValid(string[] inputFields, string validUserName)
        {
            var dateIssue = inputFields[0].IsValidOldDateTime();
            if (string.IsNullOrEmpty(dateIssue) == false) { }

            var transaction = new Transaction();
            /*{
                UserName = validUserName,
                DateIncurred = inputFields[0],
                Description = inputFields[1],
                Debit = new Money(debitRate, inputFields[0], inputFields[3], debitRate.Convert(externalDebitAmount)),
                Credit = new Money(creditRate, inputFields[0], inputFields[5], creditRate.Convert(externalCreditAmount)),
                MaskedCardNumber = inputFields[6]
            };*/

            // TODO:
            return transaction.Validate(null);
        }
    }
}
