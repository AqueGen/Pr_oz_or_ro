namespace Kapitalist.Data.Store.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DraftTenderContactPoints : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DraftTenderContactPoints",
                c => new
                {
                    TenderId = c.Int(nullable: false),
                    ContactPointId = c.Int(nullable: false),
                    SortingOrder = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.TenderId, t.ContactPointId })
                .ForeignKey("dbo.UserOrganizationContactPoints", t => t.ContactPointId)
                .ForeignKey("dbo.DraftTenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.ContactPointId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.DraftTenderContactPoints", "TenderId", "dbo.DraftTenders");
            DropForeignKey("dbo.DraftTenderContactPoints", "ContactPointId", "dbo.UserOrganizationContactPoints");
            DropIndex("dbo.DraftTenderContactPoints", new[] { "ContactPointId" });
            DropIndex("dbo.DraftTenderContactPoints", new[] { "TenderId" });
            DropTable("dbo.DraftTenderContactPoints");
        }
    }
}