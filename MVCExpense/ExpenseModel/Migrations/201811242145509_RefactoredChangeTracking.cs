namespace ExpenseModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoredChangeTracking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrackedChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OccurredAt = c.DateTime(),
                        ChangedBy = c.String(maxLength: 255),
                        ChangedTo = c.Int(nullable: false),
                        Account_UserId = c.String(maxLength: 255),
                        Account_MaskedCardNumber = c.String(maxLength: 16),
                        BankImport_Id = c.Int(),
                        File_Id = c.Int(),
                        User_Id = c.String(maxLength: 255),
                        Statement_Id = c.Int(),
                        ISO4217Currency_Id = c.String(maxLength: 3),
                        Attachment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => new { t.Account_UserId, t.Account_MaskedCardNumber })
                .ForeignKey("dbo.BankImport", t => t.BankImport_Id)
                .ForeignKey("dbo.File", t => t.File_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.Statement", t => t.Statement_Id)
                .ForeignKey("dbo.ISO4217Currency", t => t.ISO4217Currency_Id)
                .ForeignKey("dbo.Attachment", t => t.Attachment_Id)
                .Index(t => new { t.Account_UserId, t.Account_MaskedCardNumber })
                .Index(t => t.BankImport_Id)
                .Index(t => t.File_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Statement_Id)
                .Index(t => t.ISO4217Currency_Id)
                .Index(t => t.Attachment_Id);
            
            DropColumn("dbo.Account", "Created");
            DropColumn("dbo.Account", "CreatedBy");
            DropColumn("dbo.Account", "Modified");
            DropColumn("dbo.Account", "ModifiedBy");
            DropColumn("dbo.Account", "InactiveSince");
            DropColumn("dbo.BankImport", "Created");
            DropColumn("dbo.BankImport", "CreatedBy");
            DropColumn("dbo.BankImport", "Modified");
            DropColumn("dbo.BankImport", "ModifiedBy");
            DropColumn("dbo.BankImport", "InactiveSince");
            DropColumn("dbo.File", "Created");
            DropColumn("dbo.File", "CreatedBy");
            DropColumn("dbo.File", "Modified");
            DropColumn("dbo.File", "ModifiedBy");
            DropColumn("dbo.File", "InactiveSince");
            DropColumn("dbo.User", "Created");
            DropColumn("dbo.User", "CreatedBy");
            DropColumn("dbo.User", "Modified");
            DropColumn("dbo.User", "ModifiedBy");
            DropColumn("dbo.User", "InactiveSince");
            DropColumn("dbo.Statement", "Created");
            DropColumn("dbo.Statement", "CreatedBy");
            DropColumn("dbo.Statement", "Modified");
            DropColumn("dbo.Statement", "ModifiedBy");
            DropColumn("dbo.Statement", "InactiveSince");
            DropColumn("dbo.ISO4217Currency", "Created");
            DropColumn("dbo.ISO4217Currency", "CreatedBy");
            DropColumn("dbo.ISO4217Currency", "Modified");
            DropColumn("dbo.ISO4217Currency", "ModifiedBy");
            DropColumn("dbo.ISO4217Currency", "InactiveSince");
            DropColumn("dbo.Attachment", "Created");
            DropColumn("dbo.Attachment", "CreatedBy");
            DropColumn("dbo.Attachment", "Modified");
            DropColumn("dbo.Attachment", "ModifiedBy");
            DropColumn("dbo.Attachment", "InactiveSince");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attachment", "InactiveSince", c => c.DateTime());
            AddColumn("dbo.Attachment", "ModifiedBy", c => c.String(maxLength: 255));
            AddColumn("dbo.Attachment", "Modified", c => c.DateTime());
            AddColumn("dbo.Attachment", "CreatedBy", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Attachment", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.ISO4217Currency", "InactiveSince", c => c.DateTime());
            AddColumn("dbo.ISO4217Currency", "ModifiedBy", c => c.String(maxLength: 255));
            AddColumn("dbo.ISO4217Currency", "Modified", c => c.DateTime());
            AddColumn("dbo.ISO4217Currency", "CreatedBy", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.ISO4217Currency", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Statement", "InactiveSince", c => c.DateTime());
            AddColumn("dbo.Statement", "ModifiedBy", c => c.String(maxLength: 255));
            AddColumn("dbo.Statement", "Modified", c => c.DateTime());
            AddColumn("dbo.Statement", "CreatedBy", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Statement", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.User", "InactiveSince", c => c.DateTime());
            AddColumn("dbo.User", "ModifiedBy", c => c.String(maxLength: 255));
            AddColumn("dbo.User", "Modified", c => c.DateTime());
            AddColumn("dbo.User", "CreatedBy", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.User", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.File", "InactiveSince", c => c.DateTime());
            AddColumn("dbo.File", "ModifiedBy", c => c.String(maxLength: 255));
            AddColumn("dbo.File", "Modified", c => c.DateTime());
            AddColumn("dbo.File", "CreatedBy", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.File", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.BankImport", "InactiveSince", c => c.DateTime());
            AddColumn("dbo.BankImport", "ModifiedBy", c => c.String(maxLength: 255));
            AddColumn("dbo.BankImport", "Modified", c => c.DateTime());
            AddColumn("dbo.BankImport", "CreatedBy", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.BankImport", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Account", "InactiveSince", c => c.DateTime());
            AddColumn("dbo.Account", "ModifiedBy", c => c.String(maxLength: 255));
            AddColumn("dbo.Account", "Modified", c => c.DateTime());
            AddColumn("dbo.Account", "CreatedBy", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Account", "Created", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.TrackedChanges", "Attachment_Id", "dbo.Attachment");
            DropForeignKey("dbo.TrackedChanges", "ISO4217Currency_Id", "dbo.ISO4217Currency");
            DropForeignKey("dbo.TrackedChanges", "Statement_Id", "dbo.Statement");
            DropForeignKey("dbo.TrackedChanges", "User_Id", "dbo.User");
            DropForeignKey("dbo.TrackedChanges", "File_Id", "dbo.File");
            DropForeignKey("dbo.TrackedChanges", "BankImport_Id", "dbo.BankImport");
            DropForeignKey("dbo.TrackedChanges", new[] { "Account_UserId", "Account_MaskedCardNumber" }, "dbo.Account");
            DropIndex("dbo.TrackedChanges", new[] { "Attachment_Id" });
            DropIndex("dbo.TrackedChanges", new[] { "ISO4217Currency_Id" });
            DropIndex("dbo.TrackedChanges", new[] { "Statement_Id" });
            DropIndex("dbo.TrackedChanges", new[] { "User_Id" });
            DropIndex("dbo.TrackedChanges", new[] { "File_Id" });
            DropIndex("dbo.TrackedChanges", new[] { "BankImport_Id" });
            DropIndex("dbo.TrackedChanges", new[] { "Account_UserId", "Account_MaskedCardNumber" });
            DropTable("dbo.TrackedChanges");
        }
    }
}
