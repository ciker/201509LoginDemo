namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Url = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 250),
                        AccountType = c.Int(nullable: false),
                        AccountStatus = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UpdationDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 250),
                        ContactType = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UpdationDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.AccountId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contact", new[] { "AccountId" });
            DropForeignKey("dbo.Contact", "AccountId", "dbo.Account");
            DropTable("dbo.Contact");
            DropTable("dbo.Account");
        }
    }
}
