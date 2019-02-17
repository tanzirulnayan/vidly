namespace vidlyDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnnonationAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BorrowHistories", "BorrowStatus", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "UserType", c => c.String(nullable: false));
            AlterColumn("dbo.Movies", "Genre", c => c.String(nullable: false));
            AlterColumn("dbo.Moderators", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Moderators", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Moderators", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Moderators", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Moderators", "UserType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Moderators", "UserType", c => c.String());
            AlterColumn("dbo.Moderators", "Address", c => c.String());
            AlterColumn("dbo.Moderators", "Email", c => c.String());
            AlterColumn("dbo.Moderators", "Password", c => c.String());
            AlterColumn("dbo.Moderators", "Name", c => c.String());
            AlterColumn("dbo.Movies", "Genre", c => c.String());
            AlterColumn("dbo.Customers", "UserType", c => c.String());
            AlterColumn("dbo.Customers", "Address", c => c.String());
            AlterColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "Password", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
            AlterColumn("dbo.BorrowHistories", "BorrowStatus", c => c.String());
        }
    }
}
