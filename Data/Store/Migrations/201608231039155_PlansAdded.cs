namespace Kapitalist.Data.Store.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PlansAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanClassifications",
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
                .ForeignKey("dbo.Plans", t => t.PlanId, cascadeDelete: true)
                .Index(t => t.PlanId);

            CreateTable(
                "dbo.Plans",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Guid = c.Guid(nullable: false),
                    Identifier = c.String(),
                    CPV_Id = c.String(maxLength: 32),
                    CPV_Description = c.String(),
                    DatePublished = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Budget_Id = c.String(nullable: false, maxLength: 32),
                    Budget_AmountNet = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Budget_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Budget_Currency = c.String(nullable: false, maxLength: 3),
                    Budget_Description = c.String(nullable: false),
                    Budget_Project_Id = c.String(maxLength: 128),
                    Budget_Project_Name = c.String(),
                    Tender_Status = c.String(maxLength: 32),
                    Tender_ProcurementMethod = c.String(maxLength: 32),
                    Tender_ProcurementMethodRationale = c.String(),
                    Tender_ProcurementMethodType = c.String(maxLength: 32),
                    Tender_TenderPeriod_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    Tender_TenderPeriod_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    Owner = c.String(maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Guid, unique: true);

            CreateTable(
                "dbo.PlanItems",
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
                .ForeignKey("dbo.Plans", t => t.PlanId, cascadeDelete: true)
                .Index(t => t.PlanId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.PlanItemClassifications",
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
                .ForeignKey("dbo.PlanItems", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);

            CreateTable(
                "dbo.PlanProcuringEntities",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Name = c.String(),
                    Kind = c.Int(),
                    Address_StreetAddress = c.String(),
                    Address_Locality = c.String(),
                    Address_Region = c.String(maxLength: 128),
                    Address_PostalCode = c.String(maxLength: 128),
                    Address_CountryName = c.String(maxLength: 128),
                    ContactPoint_Name = c.String(maxLength: 256),
                    СontactPoint_Email = c.String(maxLength: 256),
                    СontactPoint_Telephone = c.String(maxLength: 256),
                    СontactPoint_FaxNumber = c.String(maxLength: 256),
                    СontactPoint_Url = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Plans", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.PlanProcuringEntityIdentifiers",
                c => new
                {
                    InternalId = c.Int(nullable: false, identity: true),
                    OrganizationId = c.Int(nullable: false),
                    Main = c.Boolean(nullable: false),
                    Scheme = c.String(maxLength: 32),
                    Id = c.String(nullable: false, maxLength: 32),
                    LegalName = c.String(),
                    Uri = c.String(),
                })
                .PrimaryKey(t => t.InternalId)
                .ForeignKey("dbo.PlanProcuringEntities", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.PlanProcuringEntities", "Id", "dbo.Plans");
            DropForeignKey("dbo.PlanProcuringEntityIdentifiers", "OrganizationId", "dbo.PlanProcuringEntities");
            DropForeignKey("dbo.PlanItems", "PlanId", "dbo.Plans");
            DropForeignKey("dbo.PlanItemClassifications", "ItemId", "dbo.PlanItems");
            DropForeignKey("dbo.PlanClassifications", "PlanId", "dbo.Plans");
            DropIndex("dbo.PlanProcuringEntityIdentifiers", new[] { "OrganizationId" });
            DropIndex("dbo.PlanProcuringEntities", new[] { "Id" });
            DropIndex("dbo.PlanItemClassifications", new[] { "ItemId" });
            DropIndex("dbo.PlanItems", new[] { "StringId" });
            DropIndex("dbo.PlanItems", new[] { "PlanId" });
            DropIndex("dbo.Plans", new[] { "Guid" });
            DropIndex("dbo.PlanClassifications", new[] { "PlanId" });
            DropTable("dbo.PlanProcuringEntityIdentifiers");
            DropTable("dbo.PlanProcuringEntities");
            DropTable("dbo.PlanItemClassifications");
            DropTable("dbo.PlanItems");
            DropTable("dbo.Plans");
            DropTable("dbo.PlanClassifications");
        }
    }
}