namespace OpenOrderFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedzipcode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Zipcode", c => c.String());
            AddColumn("dbo.Orders", "Expiration", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "PostalCode", c => c.String());
            DropColumn("dbo.Orders", "Experation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Experation", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "PostalCode", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Orders", "Username", c => c.String());
            DropColumn("dbo.Orders", "Expiration");
            DropColumn("dbo.Orders", "Zipcode");
        }
    }
}
