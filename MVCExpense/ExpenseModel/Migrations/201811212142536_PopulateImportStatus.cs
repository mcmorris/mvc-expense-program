namespace ExpenseModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class PopulateImportStatus : DbMigration
    {
        public override void Up()
        {
            this.Sql("INSERT INTO ImportStatus (Name, Active) VALUES ('Pending', 1);");
            this.Sql("INSERT INTO ImportStatus (Name, Active) VALUES ('Successful with Errors', 1);");
            this.Sql("INSERT INTO ImportStatus (Name, Active) VALUES ('Successful', 1);");
            this.Sql("INSERT INTO ImportStatus (Name, Active) VALUES ('Denied', 1);");
            this.Sql("INSERT INTO ImportStatus (Name, Active) VALUES ('Approved', 1);");
        }
        
        public override void Down()
        {
            this.Sql("DELETE FROM ImportStatus WHERE Name = 'Pending';");
            this.Sql("DELETE FROM ImportStatus WHERE Name = 'Successful with Errors';");
            this.Sql("DELETE FROM ImportStatus WHERE Name = 'Successful';");
            this.Sql("DELETE FROM ImportStatus WHERE Name = Denied';");
            this.Sql("DELETE FROM ImportStatus WHERE Name = 'Approved';");
        }
    }
}
