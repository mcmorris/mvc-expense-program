namespace ExpenseModel
{
    public class InvalidTransaction
    {
        public int    Id                 { get; set; }
        public BankImport BankImport     { get; set; }
        public string UserName           { get; set; }
        public string DateIncurred       { get; set; }
        public string Description        { get; set; }
        public string DebitCurrencyCode  { get; set; }
        public string DebitValue         { get; set; }
        public string CreditCurrencyCode { get; set; }
        public string CreditValue        { get; set; }
        public string Issue              { get; set; }
        public string MaskedCardNumber   { get; set; }
        public string CardExpiry         { get; set; }

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