namespace IT_Project_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModels : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.User", newName: "Users");
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        TaskName = c.String(),
                        TaskDescription = c.String(),
                        TaskStartDate = c.DateTime(nullable: false),
                        TaskEndDate = c.DateTime(nullable: false),
                        TaskStatus = c.String(),
                        AssignedUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskID);
            
            AlterColumn("dbo.Projects", "ProjectKey", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.Projects", "ProjectName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Projects", "ProjectDescription", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Projects", "ProjectStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "ProjectStatus", c => c.String());
            AlterColumn("dbo.Projects", "ProjectDescription", c => c.String());
            AlterColumn("dbo.Projects", "ProjectName", c => c.String());
            AlterColumn("dbo.Projects", "ProjectKey", c => c.String());
            DropTable("dbo.Tasks");
            RenameTable(name: "dbo.Users", newName: "User");
        }
    }
}
