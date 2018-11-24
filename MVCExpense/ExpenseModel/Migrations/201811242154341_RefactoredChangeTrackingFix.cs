namespace ExpenseModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoredChangeTrackingFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChangeStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, name: "IDX_ChangeStatusName");
            
            AddColumn("dbo.TrackedChanges", "ChangedTo_Id", c => c.Int());
            CreateIndex("dbo.TrackedChanges", "ChangedTo_Id");
            AddForeignKey("dbo.TrackedChanges", "ChangedTo_Id", "dbo.ChangeStatus", "Id");
            DropColumn("dbo.TrackedChanges", "ChangedTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrackedChanges", "ChangedTo", c => c.Int(nullable: false));
            DropForeignKey("dbo.TrackedChanges", "ChangedTo_Id", "dbo.ChangeStatus");
            DropIndex("dbo.ChangeStatus", "IDX_ChangeStatusName");
            DropIndex("dbo.TrackedChanges", new[] { "ChangedTo_Id" });
            DropColumn("dbo.TrackedChanges", "ChangedTo_Id");
            DropTable("dbo.ChangeStatus");
        }
    }
}
