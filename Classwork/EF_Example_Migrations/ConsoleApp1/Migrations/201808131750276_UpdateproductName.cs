namespace ConsoleApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateproductName : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Products", "Name", "ProductName");
            
           
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Products", "ProductName", "Name");
        }
    }
}
