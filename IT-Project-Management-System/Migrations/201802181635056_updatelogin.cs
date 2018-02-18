namespace IT_Project_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelogin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserPassword", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UserPassword", c => c.Int(nullable: false));
        }
    }
}
