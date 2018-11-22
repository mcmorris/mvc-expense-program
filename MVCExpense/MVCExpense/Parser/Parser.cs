namespace MVCExpense.Parser
{
    using System;
    using System.Collections.Generic;

    using ExpenseModel;

    public abstract class Parser
    {
        public ICollection<Transaction> ValidTransactions { get; protected set; }
        public ICollection<InvalidTransaction> InvalidTransactions { get; protected set; }
        protected const int FieldsPerLine = 6;

        public Parser()
        {
            this.ValidTransactions = new List<Transaction>();
            this.InvalidTransactions = new List<InvalidTransaction>();
        }

        public void ParseLine(string[] fields)
        {
            fields = this.SanitizeFields(fields);
            var issueMessage = Transaction.IsValid(fields);
            if (issueMessage == null)
            {
                // FIXME: Must check for CC -> User mapping validity here
                this.ValidTransactions.Add(new Transaction(fields));
                return;
            }

            this.InvalidTransactions.Add(new InvalidTransaction(fields, issueMessage));
        }

        public string[] SanitizeFields(string[] fields)
        {
            // FIXME: Not implemented yet.
            return fields;
        }

        // Format: DateIncurred, Description, [DebitCode, DebitAmount], [DebitCode, DebitAmount], MaskedCardNumber
        protected bool IsLineValid(string[] fields)
        {
            if (fields.Length != Parser.FieldsPerLine) { return false; }

            /*
            4. DebitCurrency must be a positive decimal or null, and map to a valid currency
            5. CreditCurrency must be a positive decimal or null, and map to a valid currency
            6. Only one of Debit or Credit Currency can be provided, not both

            7. MaskedCreditNumber must be masked, not expired at time of DateIncurred, and in Credit card mapping.
            8. If MaskedCreditNumber does not map to a valid user, reject.
            9. If MaskedCreditNumber does not map to a user active at DateIncurred, reject.
            */

            return true;
        }

        public virtual void ParseData(string filePath)
        {
            throw new NotSupportedException("Data structure parsing must be handled outside abstract class Parser.");
        }
    }
}