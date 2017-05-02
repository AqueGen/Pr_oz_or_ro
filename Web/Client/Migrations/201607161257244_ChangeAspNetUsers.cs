using System.Data.Entity.Migrations;

namespace Kapitalist.Web.Client.Migrations
{
    public partial class ChangeAspNetUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserOrganizationId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserOrganizationId");
        }
    }
}
