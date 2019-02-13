namespace vidlyDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keysAdded : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BorrowHistories", "MovieId");
            CreateIndex("dbo.BorrowHistories", "CustomerId");
            AddForeignKey("dbo.BorrowHistories", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BorrowHistories", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowHistories", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.BorrowHistories", "CustomerId", "dbo.Customers");
            DropIndex("dbo.BorrowHistories", new[] { "CustomerId" });
            DropIndex("dbo.BorrowHistories", new[] { "MovieId" });
        }
    }
}
