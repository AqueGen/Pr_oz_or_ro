namespace Kapitalist.Data.Store.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FeatureStringId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Features", "StringId", c => c.String(nullable: false, maxLength: 64));
            AddColumn("dbo.DraftFeatures", "StringId", c => c.String(nullable: false, maxLength: 64));
            AddColumn("dbo.DraftFeatures", "RelatedItem", c => c.String(maxLength: 64));
            AlterColumn("dbo.BidDocuments", "RelatedItem", c => c.String(maxLength: 64));
            AlterColumn("dbo.TenderComplaintDocuments", "RelatedItem", c => c.String(maxLength: 64));
            AlterColumn("dbo.CancellationDocuments", "RelatedItem", c => c.String(maxLength: 64));
            AlterColumn("dbo.ContractDocuments", "RelatedItem", c => c.String(maxLength: 64));
            AlterColumn("dbo.TenderDocuments", "RelatedItem", c => c.String(maxLength: 64));
            AlterColumn("dbo.Features", "RelatedItem", c => c.String(maxLength: 64));
            AlterColumn("dbo.Questions", "RelatedItem", c => c.String(maxLength: 64));
            AlterColumn("dbo.AwardDocuments", "RelatedItem", c => c.String(maxLength: 64));
            AlterColumn("dbo.AwardComplaintDocuments", "RelatedItem", c => c.String(maxLength: 64));
            Sql("UPDATE dbo.Features SET StringId = Code");
            CreateIndex("dbo.Features", "StringId");
            CreateIndex("dbo.DraftFeatures", "StringId");
            DropColumn("dbo.Features", "Code");
            DropColumn("dbo.DraftFeatures", "RelatedId");
        }

        public override void Down()
        {
            AddColumn("dbo.DraftFeatures", "RelatedId", c => c.Int());
            AddColumn("dbo.Features", "Code", c => c.String(nullable: false, maxLength: 128));
            DropIndex("dbo.DraftFeatures", new[] { "StringId" });
            DropIndex("dbo.Features", new[] { "StringId" });
            AlterColumn("dbo.AwardComplaintDocuments", "RelatedItem", c => c.String());
            AlterColumn("dbo.AwardDocuments", "RelatedItem", c => c.String());
            AlterColumn("dbo.Questions", "RelatedItem", c => c.String());
            AlterColumn("dbo.Features", "RelatedItem", c => c.String());
            AlterColumn("dbo.TenderDocuments", "RelatedItem", c => c.String());
            AlterColumn("dbo.ContractDocuments", "RelatedItem", c => c.String());
            AlterColumn("dbo.CancellationDocuments", "RelatedItem", c => c.String());
            AlterColumn("dbo.TenderComplaintDocuments", "RelatedItem", c => c.String());
            AlterColumn("dbo.BidDocuments", "RelatedItem", c => c.String());
            DropColumn("dbo.DraftFeatures", "RelatedItem");
            DropColumn("dbo.DraftFeatures", "StringId");
            DropColumn("dbo.Features", "StringId");
        }
    }
}