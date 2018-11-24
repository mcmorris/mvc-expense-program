namespace ExpenseModel
{
    using System;
    using System.Data.Entity;

    public class ExpensesModel : DbContext
    {
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
        public virtual DbSet<Account>            Accounts            { get; set; }
        public virtual DbSet<Attachment>         Attachments         { get; set; }
        public virtual DbSet<BankImport>         BankImports         { get; set; }
        public virtual DbSet<ExchangeRate>       ExchangeRates       { get; set; }
        public virtual DbSet<File>               Files               { get; set; }
        public virtual DbSet<ImportStatus>       ImportStatus        { get; set; }
        public virtual DbSet<InvalidTransaction> InvalidTransactions { get; set; }
        public virtual DbSet<ISO4217Currency>    Currencies          { get; set; }
        public virtual DbSet<Money>              Monies              { get; set; }
        public virtual DbSet<Statement>          Statements          { get; set; }
        public virtual DbSet<Transaction>        Transactions        { get; set; }
        public virtual DbSet<User>               Users               { get; set; }

        public ExpensesModel()
            : base("name=ExpensesModel")
        {

        }

        public override int SaveChanges()
        {
            foreach (var item in this.ChangeTracker.Entries())
            {
                if (!(item.Entity is TrackedSelfValidatorEntity modelBase)) { continue; }

                switch (item.State)
                {
                    case EntityState.Added:
                        modelBase.TrackChange(DateTime.UtcNow, "TODO: THIS USER", ChangeTrackingType.Created);
                        // TODO: modelBase.CreatedBy = GetUser();
                        break;
                    case EntityState.Modified:
                        modelBase.TrackChange(DateTime.UtcNow, "TODO: THIS USER", ChangeTrackingType.Modified);
                        // TODO: modelBase.ModifiedBy = GetUser();
                        // TODO: If modelBase.Active was true and is now false: modelBase.InactiveSince = DateTime.UtcNow;                        
                        break;
                }
            }

            return base.SaveChanges();
        }

    }
}