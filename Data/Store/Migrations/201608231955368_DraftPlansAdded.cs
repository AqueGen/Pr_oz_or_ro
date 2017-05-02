namespace Kapitalist.Data.Store.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DraftPlansAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DraftPlanClassifications",
                c => new
                {
                    InternalId = c.Int(nullable: false, identity: true),
                    PlanId = c.Int(nullable: false),
                    Scheme = c.String(maxLength: 32),
                    Id = c.String(maxLength: 32),
                    Description = c.String(),
                    Uri = c.String(),
                })
                .PrimaryKey(t => t.InternalId)
                .ForeignKey("dbo.DraftPlans", t => t.PlanId, cascadeDelete: true)
                .Index(t => t.PlanId);

            CreateTable(
                "dbo.DraftPlans",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Guid = c.Guid(nullable: false),
                    ProcuringEntityId = c.Int(nullable: false),
                    CPV_Id = c.String(maxLength: 32),
                    CPV_Description = c.String(),
                    DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Budget_Id = c.String(nullable: false, maxLength: 32),
                    Budget_Year = c.Int(),
                    Budget_AmountNet = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Budget_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Budget_Currency = c.String(nullable: false, maxLength: 3),
                    Budget_Description = c.String(nullable: false),
                    Budget_Notes = c.String(),
                    Budget_Project_Id = c.String(maxLength: 128),
                    Budget_Project_Name = c.String(),
                    Tender_Status = c.String(maxLength: 32),
                    Tender_ProcurementMethod = c.String(maxLength: 32),
                    Tender_ProcurementMethodRationale = c.String(),
                    Tender_ProcurementMethodType = c.String(maxLength: 32),
                    Tender_TenderPeriod_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    Tender_TenderPeriod_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserOrganizations", t => t.ProcuringEntityId, cascadeDelete: true)
                .Index(t => t.ProcuringEntityId)
                .Index(t => t.Guid, unique: true);

            CreateTable(
                "dbo.DraftPlanItems",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    PlanId = c.Int(nullable: false),
                    Description = c.String(),
                    CPV_Id = c.String(maxLength: 32),
                    CPV_Description = c.String(),
                    Unit_Code = c.String(maxLength: 32),
                    Unit_Name = c.String(maxLength: 128),
                    Quantity = c.Long(nullable: false),
                    DeliveryDate_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    DeliveryDate_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    DeliveryAddress_StreetAddress = c.String(),
                    DeliveryAddress_Locality = c.String(),
                    DeliveryAddress_Region = c.String(maxLength: 128),
                    DeliveryAddress_PostalCode = c.String(maxLength: 128),
                    DeliveryAddress_CountryName = c.String(maxLength: 128),
                    DeliveryLocation_Latitude = c.String(maxLength: 32),
                    DeliveryLocation_Longitude = c.String(maxLength: 32),
                    DeliveryLocation_Elevation = c.String(maxLength: 32),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DraftPlans", t => t.PlanId, cascadeDelete: true)
                .Index(t => t.PlanId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.DraftPlanItemClassifications",
                c => new
                {
                    InternalId = c.Int(nullable: false, identity: true),
                    ItemId = c.Int(nullable: false),
                    Scheme = c.String(maxLength: 32),
                    Id = c.String(maxLength: 32),
                    Description = c.String(),
                    Uri = c.String(),
                })
                .PrimaryKey(t => t.InternalId)
                .ForeignKey("dbo.DraftPlanItems", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);

            AddColumn("dbo.Plans", "Budget_Year", c => c.Int());
            AddColumn("dbo.Plans", "Budget_Notes", c => c.String());
        }

        public override void Down()
        {
            DropForeignKey("dbo.DraftPlans", "ProcuringEntityId", "dbo.UserOrganizations");
            DropForeignKey("dbo.DraftPlanItems", "PlanId", "dbo.DraftPlans");
            DropForeignKey("dbo.DraftPlanItemClassifications", "ItemId", "dbo.DraftPlanItems");
            DropForeignKey("dbo.DraftPlanClassifications", "PlanId", "dbo.DraftPlans");
            DropIndex("dbo.DraftPlanItemClassifications", new[] { "ItemId" });
            DropIndex("dbo.DraftPlanItems", new[] { "StringId" });
            DropIndex("dbo.DraftPlanItems", new[] { "PlanId" });
            DropIndex("dbo.DraftPlans", new[] { "Guid" });
            DropIndex("dbo.DraftPlans", new[] { "ProcuringEntityId" });
            DropIndex("dbo.DraftPlanClassifications", new[] { "PlanId" });
            DropColumn("dbo.Plans", "Budget_Notes");
            DropColumn("dbo.Plans", "Budget_Year");
            DropTable("dbo.DraftPlanItemClassifications");
            DropTable("dbo.DraftPlanItems");
            DropTable("dbo.DraftPlans");
            DropTable("dbo.DraftPlanClassifications");
        }
    }
}