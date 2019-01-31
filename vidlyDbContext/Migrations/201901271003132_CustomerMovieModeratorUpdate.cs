namespace vidlyDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerMovieModeratorUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "UserType", c => c.String());
            AddColumn("dbo.Moderators", "UserType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Moderators", "UserType");
            DropColumn("dbo.Customers", "UserType");
        }
    }
}
