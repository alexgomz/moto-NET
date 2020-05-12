namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseIsActivePropertyAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "isActive");
        }
    }
}
