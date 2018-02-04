namespace IT_Project_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String(maxLength: 14));
            AlterColumn("dbo.Users", "UserType", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Language", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Address", c => c.String());
            AlterColumn("dbo.Users", "Language", c => c.String());
            AlterColumn("dbo.Users", "UserType", c => c.String());
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
        }
    }
}
