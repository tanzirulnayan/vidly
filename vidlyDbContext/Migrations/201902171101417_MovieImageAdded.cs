namespace vidlyDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieImageAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "ImagePath", c => c.String());
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Name", c => c.String());
            DropColumn("dbo.Movies", "ImagePath");
        }
    }
}
