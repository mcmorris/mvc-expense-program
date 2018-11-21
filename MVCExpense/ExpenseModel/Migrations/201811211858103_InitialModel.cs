namespace ExpenseModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 255),
                        MaskedCardNumber = c.String(nullable: false, maxLength: 16),
                        Expiry = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 255),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 255),
                        InactiveSince = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MaskedCardNumber })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Expiry, name: "IDX_CCExpiry");
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatementId = c.Int(nullable: false),
                        BankImportId = c.Int(nullable: false),
                        DebitId = c.Int(),
                        CreditId = c.Int(),
                        UserName = c.String(nullable: false, maxLength: 255),
                        DateIncurred = c.DateTime(nullable: false),
                        Description = c.String(),
                        MaskedCardNumber = c.String(nullable: false, maxLength: 16),
                        Account_UserId = c.String(maxLength: 255),
                        Account_MaskedCardNumber = c.String(maxLength: 16),
                        Attachment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankImport", t => t.BankImportId, cascadeDelete: true)
                .ForeignKey("dbo.Money", t => t.CreditId)
                .ForeignKey("dbo.Money", t => t.DebitId)
                .ForeignKey("dbo.Statement", t => t.StatementId, cascadeDelete: true)
                .ForeignKey("dbo.Account", t => new { t.Account_UserId, t.Account_MaskedCardNumber })
                .ForeignKey("dbo.Attachment", t => t.Attachment_Id)
                .Index(t => t.StatementId, name: "IDX_TransactionStatementId")
                .Index(t => t.BankImportId, name: "IDX_TransactionBankImportId")
                .Index(t => t.DebitId)
                .Index(t => t.CreditId)
                .Index(t => new { t.Account_UserId, t.Account_MaskedCardNumber })
                .Index(t => t.Attachment_Id);
            
            CreateTable(
                "dbo.BankImport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatementId = c.Int(),
                        FileId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 255),
                        ImportStatusId = c.Int(nullable: false),
                        Bank = c.String(maxLength: 255),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 255),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 255),
                        InactiveSince = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.File", t => t.FileId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.ImportStatus", t => t.ImportStatusId, cascadeDelete: true)
                .ForeignKey("dbo.Statement", t => t.StatementId)
                .Index(t => t.StatementId, name: "IDX_BankImportStatementId")
                .Index(t => t.FileId)
                .Index(t => t.UserId)
                .Index(t => t.ImportStatusId);
            
            CreateTable(
                "dbo.File",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 255),
                        FileName = c.String(nullable: false, maxLength: 255),
                        FilePath = c.String(nullable: false),
                        ContentType = c.String(maxLength: 255),
                        Description = c.String(),
                        FileSize = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 255),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 255),
                        InactiveSince = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId, name: "IDX_FileUserId");
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        ManagerId = c.String(maxLength: 255),
                        FullName = c.String(nullable: false, maxLength: 255),
                        DepartmentName = c.String(maxLength: 255),
                        JobTitle = c.String(maxLength: 255),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 255),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 255),
                        InactiveSince = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImportStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, name: "IDX_ImportStatusName");
            
            CreateTable(
                "dbo.InvalidTransaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankImportId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 255),
                        DateIncurred = c.String(maxLength: 255),
                        Description = c.String(),
                        DebitCurrencyCode = c.String(maxLength: 3),
                        DebitValue = c.String(maxLength: 255),
                        CreditCurrencyCode = c.String(maxLength: 3),
                        CreditValue = c.String(maxLength: 255),
                        Issue = c.String(maxLength: 255),
                        MaskedCardNumber = c.String(maxLength: 16),
                        CardExpiry = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankImport", t => t.BankImportId, cascadeDelete: true)
                .Index(t => t.BankImportId, name: "IDX_InvalidTransactionBankImportId");
            
            CreateTable(
                "dbo.Statement",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImportStatusId = c.Int(),
                        Month = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 255),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 255),
                        InactiveSince = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImportStatus", t => t.ImportStatusId)
                .Index(t => t.ImportStatusId);
            
            CreateTable(
                "dbo.Money",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExchangeRateId = c.Int(nullable: false),
                        IncurredOn = c.DateTime(nullable: false),
                        ExternalAmount = c.Decimal(nullable: false, precision: 18, scale: 4),
                        InternalAmount = c.Decimal(nullable: false, precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExchangeRate", t => t.ExchangeRateId, cascadeDelete: true)
                .Index(t => t.ExchangeRateId)
                .Index(t => t.InternalAmount, name: "IDX_MoneyInternalAmount");
            
            CreateTable(
                "dbo.ExchangeRate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrencyFromId = c.String(maxLength: 3),
                        CurrencyToId = c.String(maxLength: 3),
                        Effective = c.DateTime(nullable: false),
                        ConversionRate = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ISO4217Currency", t => t.CurrencyFromId)
                .ForeignKey("dbo.ISO4217Currency", t => t.CurrencyToId)
                .Index(t => t.CurrencyFromId)
                .Index(t => t.CurrencyToId);
            
            CreateTable(
                "dbo.ISO4217Currency",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 3),
                        Exponent = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        WithdrawalDate = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 255),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 255),
                        InactiveSince = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, name: "IDX_CurrencyName");
            
            CreateTable(
                "dbo.Attachment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileId = c.Int(nullable: false),
                        StatementId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Issue = c.String(maxLength: 255),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 255),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 255),
                        InactiveSince = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.File", t => t.FileId, cascadeDelete: true)
                .ForeignKey("dbo.Statement", t => t.StatementId, cascadeDelete: true)
                .ForeignKey("dbo.ImportStatus", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.FileId, name: "IDX_AttachmentFileId")
                .Index(t => t.StatementId, name: "IDX_AttachmentStatementId")
                .Index(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "Attachment_Id", "dbo.Attachment");
            DropForeignKey("dbo.Attachment", "StatusId", "dbo.ImportStatus");
            DropForeignKey("dbo.Attachment", "StatementId", "dbo.Statement");
            DropForeignKey("dbo.Attachment", "FileId", "dbo.File");
            DropForeignKey("dbo.Transaction", new[] { "Account_UserId", "Account_MaskedCardNumber" }, "dbo.Account");
            DropForeignKey("dbo.Transaction", "StatementId", "dbo.Statement");
            DropForeignKey("dbo.Transaction", "DebitId", "dbo.Money");
            DropForeignKey("dbo.Transaction", "CreditId", "dbo.Money");
            DropForeignKey("dbo.Money", "ExchangeRateId", "dbo.ExchangeRate");
            DropForeignKey("dbo.ExchangeRate", "CurrencyToId", "dbo.ISO4217Currency");
            DropForeignKey("dbo.ExchangeRate", "CurrencyFromId", "dbo.ISO4217Currency");
            DropForeignKey("dbo.Transaction", "BankImportId", "dbo.BankImport");
            DropForeignKey("dbo.Statement", "ImportStatusId", "dbo.ImportStatus");
            DropForeignKey("dbo.BankImport", "StatementId", "dbo.Statement");
            DropForeignKey("dbo.InvalidTransaction", "BankImportId", "dbo.BankImport");
            DropForeignKey("dbo.BankImport", "ImportStatusId", "dbo.ImportStatus");
            DropForeignKey("dbo.BankImport", "UserId", "dbo.User");
            DropForeignKey("dbo.BankImport", "FileId", "dbo.File");
            DropForeignKey("dbo.File", "UserId", "dbo.User");
            DropForeignKey("dbo.Account", "UserId", "dbo.User");
            DropIndex("dbo.Attachment", new[] { "StatusId" });
            DropIndex("dbo.Attachment", "IDX_AttachmentStatementId");
            DropIndex("dbo.Attachment", "IDX_AttachmentFileId");
            DropIndex("dbo.ISO4217Currency", "IDX_CurrencyName");
            DropIndex("dbo.ExchangeRate", new[] { "CurrencyToId" });
            DropIndex("dbo.ExchangeRate", new[] { "CurrencyFromId" });
            DropIndex("dbo.Money", "IDX_MoneyInternalAmount");
            DropIndex("dbo.Money", new[] { "ExchangeRateId" });
            DropIndex("dbo.Statement", new[] { "ImportStatusId" });
            DropIndex("dbo.InvalidTransaction", "IDX_InvalidTransactionBankImportId");
            DropIndex("dbo.ImportStatus", "IDX_ImportStatusName");
            DropIndex("dbo.File", "IDX_FileUserId");
            DropIndex("dbo.BankImport", new[] { "ImportStatusId" });
            DropIndex("dbo.BankImport", new[] { "UserId" });
            DropIndex("dbo.BankImport", new[] { "FileId" });
            DropIndex("dbo.BankImport", "IDX_BankImportStatementId");
            DropIndex("dbo.Transaction", new[] { "Attachment_Id" });
            DropIndex("dbo.Transaction", new[] { "Account_UserId", "Account_MaskedCardNumber" });
            DropIndex("dbo.Transaction", new[] { "CreditId" });
            DropIndex("dbo.Transaction", new[] { "DebitId" });
            DropIndex("dbo.Transaction", "IDX_TransactionBankImportId");
            DropIndex("dbo.Transaction", "IDX_TransactionStatementId");
            DropIndex("dbo.Account", "IDX_CCExpiry");
            DropIndex("dbo.Account", new[] { "UserId" });
            DropTable("dbo.Attachment");
            DropTable("dbo.ISO4217Currency");
            DropTable("dbo.ExchangeRate");
            DropTable("dbo.Money");
            DropTable("dbo.Statement");
            DropTable("dbo.InvalidTransaction");
            DropTable("dbo.ImportStatus");
            DropTable("dbo.User");
            DropTable("dbo.File");
            DropTable("dbo.BankImport");
            DropTable("dbo.Transaction");
            DropTable("dbo.Account");
        }
    }
}
