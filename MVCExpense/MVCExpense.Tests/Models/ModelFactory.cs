namespace MVCExpense.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    using ExpenseModel;

    public class ModelFactory : CurrencyFactory
    {
        public Account Account(User user, string maskedCardNumber, int expiryYear, int expiryMonth)
        {
            return new Account(user, maskedCardNumber, expiryYear, expiryMonth);
        }

        public Attachment Attachment(File file, string issue, StatusTypes status, Statement statement, ICollection<Transaction> transactions = null)
        {
            var attach = new Attachment(file, issue, status, statement, transactions);
            if (transactions == null)
            {
                attach.TransactionsCovered = new List<Transaction>();
            }

            return attach;
        }

        public BankImport BankImport(Statement statement, File file, User user, string bank, StatusTypes status)
        {
            return new BankImport(0, statement, file, user, bank, new ImportStatus(status), new Collection<Transaction>(), new Collection<InvalidTransaction>(), new Collection<TrackedChange>(), true);
        }

        public ExchangeRate ExchangeRate(DateTime effective, decimal rate, ISO4217Currency from, ISO4217Currency to)
        {
            return new ExchangeRate(effective, rate, from, to);
        }

        public File File(User user, string fileName, string filePath, string contentType, int fileSize)
        {
            return new File(0, user, fileName, filePath, contentType, null, fileSize, new Collection<TrackedChange>(), true);
        }

        public InvalidTransaction InvalidTransaction(BankImport import, string userName, string occurredOn, string description, string debitCurrency, string debitAmount, string creditCurrency, string creditAmount, string issue, string maskedCardNumber, string cardExpiry)
        {
            return new InvalidTransaction(
                import,
                userName,
                occurredOn,
                description,
                debitCurrency,
                debitAmount,
                creditCurrency,
                creditAmount,
                new ValidationResult(issue),
                maskedCardNumber,
                cardExpiry
            );
        }

        public Statement Statement()
        {
            return new Statement(DateTime.Now, StatusTypes.Pending);
        }

        public Transaction Transaction(string userName, DateTime occurredOn, string description, ExchangeRate debitRate, decimal debitValue, ExchangeRate creditRate, decimal creditValue, string maskedCardNumber)
        {
            return new Transaction(
                userName,
                occurredOn,
                description,
                debitRate,
                debitValue,
                creditRate,
                creditValue,
                maskedCardNumber
           );
        }

        public User User(string userName, string fullName, string department)
        {
            return new User(userName, null, fullName, department, null);
        }
    }
}