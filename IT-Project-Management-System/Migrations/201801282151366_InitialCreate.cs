namespace IT_Project_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        ProjectManagerID = c.Int(nullable: false),
                        ProjectKey = c.String(),
                        ProjectName = c.String(),
                        ProjectDescription = c.String(),
                        ProjectStartDate = c.DateTime(nullable: false),
                        ProjectEndDate = c.DateTime(nullable: false),
                        ProjectStatus = c.String(),
                    })
                .PrimaryKey(t => t.ProjectID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        UserType = c.String(),
                        Language = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
            DropTable("dbo.Projects");
        }
    }
}
