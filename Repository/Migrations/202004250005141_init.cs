namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseDate = c.DateTime(nullable: false),
                        motorcycleId = c.Int(nullable: false),
                        customerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.customerId, cascadeDelete: true)
                .ForeignKey("dbo.Motorcycles", t => t.motorcycleId, cascadeDelete: true)
                .Index(t => t.motorcycleId)
                .Index(t => t.customerId);
            
            CreateTable(
                "dbo.Motorcycles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "motorcycleId", "dbo.Motorcycles");
            DropForeignKey("dbo.Purchases", "customerId", "dbo.Customers");
            DropIndex("dbo.Purchases", new[] { "customerId" });
            DropIndex("dbo.Purchases", new[] { "motorcycleId" });
            DropTable("dbo.Motorcycles");
            DropTable("dbo.Purchases");
            DropTable("dbo.Customers");
        }
    }
}
