using System.Data.Entity.Migrations;

namespace Kapitalist.Web.Client.Migrations
{
    public partial class DeleteIsPasswordTemporary : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "IsPasswordTemporary");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsPasswordTemporary", c => c.Boolean(nullable: false));
        }
    }
}