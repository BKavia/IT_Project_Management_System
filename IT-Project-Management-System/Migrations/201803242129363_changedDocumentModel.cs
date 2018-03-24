namespace IT_Project_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedDocumentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "FileName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "FileName");
        }
    }
}
