namespace Kapitalist.Data.Store.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SyncConcurrency : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.SyncErrors");
            CreateTable(
                "dbo.SyncErrors",
                c => new
                {
                    Type = c.Int(nullable: false),
                    Guid = c.Guid(nullable: false),
                    Offset = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => new { t.Type, t.Guid });
            CreateTable(
                "dbo.SyncStates",
                c => new
                {
                    Type = c.Int(nullable: false),
                    Restoring = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Type);
            AddColumn("dbo.Tenders", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Tenders", "TitleEn", c => c.String());
            AddColumn("dbo.DraftTenders", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.DraftTenders", "TitleEn", c => c.String());
            AddColumn("dbo.DraftPlans", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Plans", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Plans", "DateSynced", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }

        public override void Down()
        {
            DropColumn("dbo.Plans", "DateSynced");
            DropColumn("dbo.Plans", "RowVersion");
            DropColumn("dbo.DraftPlans", "RowVersion");
            DropColumn("dbo.DraftTenders", "TitleEn");
            DropColumn("dbo.DraftTenders", "RowVersion");
            DropColumn("dbo.Tenders", "TitleEn");
            DropColumn("dbo.Tenders", "RowVersion");
            DropTable("dbo.SyncStates");
            DropTable("dbo.SyncErrors");
            CreateTable(
                "dbo.SyncErrors",
                c => new
                {
                    TenderGuid = c.Guid(nullable: false),
                    Offset = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.TenderGuid);
        }
    }
}