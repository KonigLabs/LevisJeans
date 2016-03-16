namespace KonigLabs.LevisJeans.Migrations.MainDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        MiddleName = c.String(),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Phrase = c.String(),
                        Checked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Answer1 = c.Boolean(nullable: false),
                        Answer2 = c.Boolean(nullable: false),
                        Answer3 = c.Boolean(nullable: false),
                        Answer4 = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "Id", "dbo.Customers");
            DropIndex("dbo.Tests", new[] { "Id" });
            DropTable("dbo.Tests");
            DropTable("dbo.Customers");
        }
    }
}
