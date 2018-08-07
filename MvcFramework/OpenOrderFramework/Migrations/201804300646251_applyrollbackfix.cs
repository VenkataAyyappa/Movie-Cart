namespace OpenOrderFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applyrollbackfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Experation", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "Username", c => c.String());
            AlterColumn("dbo.Orders", "PostalCode", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.Orders", "Zipcode");
            DropColumn("dbo.Orders", "Expiration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Expiration", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "Zipcode", c => c.String());
            AlterColumn("dbo.Orders", "PostalCode", c => c.String());
            AlterColumn("dbo.Orders", "Username", c => c.String(nullable: false));
            DropColumn("dbo.Orders", "Experation");
        }
    }
}
