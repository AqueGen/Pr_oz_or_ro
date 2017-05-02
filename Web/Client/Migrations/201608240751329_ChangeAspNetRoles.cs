using System.Data.Entity.Migrations;

namespace Kapitalist.Web.Client.Migrations
{
    public partial class ChangeAspNetRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "NameCyrillic", c => c.String(nullable: false, maxLength: 256, fixedLength: true));
            DropColumn("dbo.AspNetRoles", "NameCirilic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetRoles", "NameCirilic", c => c.String(nullable: false, maxLength: 256, fixedLength: true));
            DropColumn("dbo.AspNetRoles", "NameCyrillic");
        }
    }
}