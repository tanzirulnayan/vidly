namespace vidlyDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReturnDate_BorrowStatus_Added_In_BorrowHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BorrowHistories", "ReturnDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BorrowHistories", "BorrowStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BorrowHistories", "BorrowStatus");
            DropColumn("dbo.BorrowHistories", "ReturnDate");
        }
    }
}
