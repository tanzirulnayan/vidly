namespace vidlyDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BorrowHistoryAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BorrowHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MovieId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BorrowHistories");
        }
    }
}
