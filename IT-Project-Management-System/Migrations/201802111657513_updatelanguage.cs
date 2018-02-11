namespace IT_Project_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelanguage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "TaskName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Tasks", "TaskDescription", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Tasks", "TaskStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "TaskStatus", c => c.String());
            AlterColumn("dbo.Tasks", "TaskDescription", c => c.String());
            AlterColumn("dbo.Tasks", "TaskName", c => c.String());
        }
    }
}
