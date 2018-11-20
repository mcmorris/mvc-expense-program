namespace ExpenseModel
{
    using System;

    public class Transaction
    {
        public int Id                  { get; set; }
        public Statement Statement     { get; set; }
        public int StatementId         { get; set; }
        public BankImport BankImport   { get; set; }
        public int BankImportId        { get; set; }
        public string UserName         { get; set; }
        public DateTime DateIncurred   { get; set; }
        public string Description      { get; set; }
        public Money Debit             { get; set; }
        public Money Credit            { get; set; }
        public string MaskedCardNumber { get; set; }

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
