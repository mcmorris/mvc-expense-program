namespace MVCExpense
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ExpenseModel : DbContext
    {
        public ExpenseModel()
            : base("name=ExpenseModel")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AttachmentCoverage> AttachmentCoverages { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<AuditAll> AuditAlls { get; set; }
        public virtual DbSet<BankImport> BankImports { get; set; }
        public virtual DbSet<ConvertableCurrency> ConvertableCurrencies { get; set; }
        public virtual DbSet<CurrencySum> CurrencySums { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<ImportStatus> ImportStatus { get; set; }
        public virtual DbSet<InvalidTransaction> InvalidTransactions { get; set; }
        public virtual DbSet<Statement> Statements { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => new { e.UserName, e.MaskedCardNumber })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attachment>()
                .HasMany(e => e.AttachmentCoverages)
                .WithRequired(e => e.Attachment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BankImport>()
                .HasMany(e => e.InvalidTransactions)
                .WithRequired(e => e.BankImport)
                .HasForeignKey(e => e.ImportId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BankImport>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.BankImport)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConvertableCurrency>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<ConvertableCurrency>()
                .Property(e => e.ConversionRate)
                .HasPrecision(18, 4);

            modelBuilder.Entity<ConvertableCurrency>()
                .Property(e => e.InternalAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<ConvertableCurrency>()
                .HasMany(e => e.DebitTransactions)
                .WithOptional(e => e.ConvertableCurrency)
                .HasForeignKey(e => e.CreditCurrency);

            modelBuilder.Entity<ConvertableCurrency>()
                .HasMany(e => e.CreditTransactions)
                .WithOptional(e => e.ConvertableCurrency1)
                .HasForeignKey(e => e.DebitCurrency);

            modelBuilder.Entity<CurrencySum>()
                .Property(e => e.InternalDebitTotal)
                .HasPrecision(18, 4);

            modelBuilder.Entity<CurrencySum>()
                .Property(e => e.InternalCreditTotal)
                .HasPrecision(18, 4);

            modelBuilder.Entity<CurrencySum>()
                .Property(e => e.InternalBalanceTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CurrencySum>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.CurrencySum)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurrencySum>()
                .HasMany(e => e.BankImports)
                .WithRequired(e => e.CurrencySum)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurrencySum>()
                .HasMany(e => e.Statements)
                .WithRequired(e => e.CurrencySum)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<File>()
                .HasMany(e => e.Attachments)
                .WithRequired(e => e.File)
                .HasForeignKey(e => e.AttachmentFileId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<File>()
                .HasMany(e => e.BankImports)
                .WithRequired(e => e.File)
                .HasForeignKey(e => e.ImportFileId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImportStatus>()
                .HasMany(e => e.BankImports)
                .WithRequired(e => e.ImportStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ImportStatus>()
                .HasMany(e => e.Statements)
                .WithRequired(e => e.ImportStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Statement>()
                .HasMany(e => e.AttachmentCoverages)
                .WithOptional(e => e.Statement)
                .HasForeignKey(e => e.StatementCovered);

            modelBuilder.Entity<Statement>()
                .HasMany(e => e.BankImports)
                .WithRequired(e => e.Statement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Statement>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Statement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .HasMany(e => e.AttachmentCoverages)
                .WithOptional(e => e.Transaction)
                .HasForeignKey(e => e.TransactionCovered);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserName)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.BankImports)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.ImportedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Files)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Uploader)
                .WillCascadeOnDelete(false);
        }
    }
}
