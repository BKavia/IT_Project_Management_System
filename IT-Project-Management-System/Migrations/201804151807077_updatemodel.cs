namespace IT_Project_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "TaskName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Projects", "ProjectName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "ProjectName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tasks", "TaskName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
