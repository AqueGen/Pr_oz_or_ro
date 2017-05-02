namespace Kapitalist.Data.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AwardComplaintAuthorIdentifiers",
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
                .ForeignKey("dbo.AwardComplaintAuthors", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);

            CreateTable(
                "dbo.AwardComplaintAuthors",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Name = c.String(maxLength: 256),
                    Address_StreetAddress = c.String(),
                    Address_Locality = c.String(maxLength: 128),
                    Address_Region = c.String(maxLength: 128),
                    Address_PostalCode = c.String(maxLength: 32),
                    Address_CountryName = c.String(nullable: false, maxLength: 128),
                    СontactPoint_Name = c.String(nullable: false, maxLength: 256),
                    СontactPoint_Email = c.String(maxLength: 256),
                    СontactPoint_Telephone = c.String(maxLength: 256),
                    СontactPoint_FaxNumber = c.String(maxLength: 256),
                    СontactPoint_Url = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AwardComplaints", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.AwardComplaints",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    AwardId = c.Int(nullable: false),
                    LotId = c.Int(),
                    Title = c.String(nullable: false),
                    Description = c.String(),
                    Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DateSubmitted = c.DateTime(precision: 7, storeType: "datetime2"),
                    DateAnswered = c.DateTime(precision: 7, storeType: "datetime2"),
                    DateEscalated = c.DateTime(precision: 7, storeType: "datetime2"),
                    DateDecision = c.DateTime(precision: 7, storeType: "datetime2"),
                    DateCanceled = c.DateTime(precision: 7, storeType: "datetime2"),
                    Status = c.String(maxLength: 32),
                    Type = c.Int(nullable: false),
                    Resolution = c.String(),
                    ResolutionType = c.Int(nullable: false),
                    Satisfied = c.Boolean(nullable: false),
                    Decision = c.String(),
                    CancellationReason = c.String(),
                    TendererAction = c.String(),
                    TendererActionDate = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Awards", t => t.AwardId, cascadeDelete: true)
                .ForeignKey("dbo.Lots", t => t.LotId)
                .Index(t => t.AwardId)
                .Index(t => t.LotId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.Awards",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    TenderId = c.Int(nullable: false),
                    BidId = c.Int(),
                    LotId = c.Int(),
                    Title = c.String(),
                    Description = c.String(),
                    Status = c.String(maxLength: 32),
                    Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Value_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Value_Currency = c.String(nullable: false, maxLength: 3),
                    Value_VATIncluded = c.Boolean(nullable: false),
                    ComplaintPeriod_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    ComplaintPeriod_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .ForeignKey("dbo.Bids", t => t.BidId)
                .ForeignKey("dbo.Lots", t => t.LotId)
                .Index(t => t.TenderId)
                .Index(t => t.BidId)
                .Index(t => t.LotId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.Bids",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    TenderId = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Status = c.String(maxLength: 32),
                    Value_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Value_Currency = c.String(nullable: false, maxLength: 3),
                    Value_VATIncluded = c.Boolean(nullable: false),
                    ParticipationUrl = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.BidDocuments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    BidId = c.Int(nullable: false),
                    RelatedItem = c.String(),
                    Url = c.String(),
                    DatePublished = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DocumentType = c.String(maxLength: 32),
                    Title = c.String(),
                    Description = c.String(),
                    Format = c.String(maxLength: 128),
                    Language = c.String(maxLength: 32),
                    DocumentOf = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bids", t => t.BidId, cascadeDelete: true)
                .Index(t => t.BidId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.LotValues",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    BidId = c.Int(nullable: false),
                    LotId = c.Int(),
                    Value_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Value_Currency = c.String(nullable: false, maxLength: 3),
                    Value_VATIncluded = c.Boolean(nullable: false),
                    Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    ParticipationUrl = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bids", t => t.BidId, cascadeDelete: true)
                .ForeignKey("dbo.Lots", t => t.LotId)
                .Index(t => t.BidId)
                .Index(t => t.LotId);

            CreateTable(
                "dbo.Lots",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    TenderId = c.Int(nullable: false),
                    Title = c.String(),
                    Description = c.String(),
                    Guarantee_Amount = c.Decimal(precision: 18, scale: 2),
                    Guarantee_Currency = c.String(maxLength: 3),
                    Value_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Value_Currency = c.String(nullable: false, maxLength: 3),
                    Value_VATIncluded = c.Boolean(nullable: false),
                    MinimalStep_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    MinimalStep_Currency = c.String(nullable: false, maxLength: 3),
                    MinimalStep_VATIncluded = c.Boolean(nullable: false),
                    AuctionPeriod_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    AuctionPeriod_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    AuctionUrl = c.String(),
                    Status = c.String(maxLength: 32),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.TenderComplaints",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    TenderId = c.Int(nullable: false),
                    LotId = c.Int(),
                    Title = c.String(nullable: false),
                    Description = c.String(),
                    Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DateSubmitted = c.DateTime(precision: 7, storeType: "datetime2"),
                    DateAnswered = c.DateTime(precision: 7, storeType: "datetime2"),
                    DateEscalated = c.DateTime(precision: 7, storeType: "datetime2"),
                    DateDecision = c.DateTime(precision: 7, storeType: "datetime2"),
                    DateCanceled = c.DateTime(precision: 7, storeType: "datetime2"),
                    Status = c.String(maxLength: 32),
                    Type = c.Int(nullable: false),
                    Resolution = c.String(),
                    ResolutionType = c.Int(nullable: false),
                    Satisfied = c.Boolean(nullable: false),
                    Decision = c.String(),
                    CancellationReason = c.String(),
                    TendererAction = c.String(),
                    TendererActionDate = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lots", t => t.LotId)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.LotId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.TenderComplaintAuthors",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Name = c.String(maxLength: 256),
                    Address_StreetAddress = c.String(),
                    Address_Locality = c.String(maxLength: 128),
                    Address_Region = c.String(maxLength: 128),
                    Address_PostalCode = c.String(maxLength: 32),
                    Address_CountryName = c.String(nullable: false, maxLength: 128),
                    СontactPoint_Name = c.String(nullable: false, maxLength: 256),
                    СontactPoint_Email = c.String(maxLength: 256),
                    СontactPoint_Telephone = c.String(maxLength: 256),
                    СontactPoint_FaxNumber = c.String(maxLength: 256),
                    СontactPoint_Url = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TenderComplaints", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.TenderComplaintAuthorIdentifiers",
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
                .ForeignKey("dbo.TenderComplaintAuthors", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);

            CreateTable(
                "dbo.TenderComplaintDocuments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    ComplaintId = c.Int(nullable: false),
                    RelatedItem = c.String(),
                    Url = c.String(),
                    DatePublished = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DocumentType = c.String(maxLength: 32),
                    Title = c.String(),
                    Description = c.String(),
                    Format = c.String(maxLength: 128),
                    Language = c.String(maxLength: 32),
                    DocumentOf = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TenderComplaints", t => t.ComplaintId, cascadeDelete: true)
                .Index(t => t.ComplaintId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.Tenders",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Guid = c.Guid(nullable: false),
                    Identifier = c.String(),
                    Title = c.String(),
                    Description = c.String(),
                    DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Guarantee_Amount = c.Decimal(precision: 18, scale: 2),
                    Guarantee_Currency = c.String(maxLength: 3),
                    Value_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Value_Currency = c.String(nullable: false, maxLength: 3),
                    Value_VATIncluded = c.Boolean(nullable: false),
                    MinimalStep_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    MinimalStep_Currency = c.String(nullable: false, maxLength: 3),
                    MinimalStep_VATIncluded = c.Boolean(nullable: false),
                    EnquiryPeriod_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    EnquiryPeriod_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    TenderPeriod_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    TenderPeriod_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    AuctionPeriod_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    AuctionPeriod_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    AwardPeriod_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    AwardPeriod_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    AuctionUrl = c.String(),
                    Status = c.String(maxLength: 32),
                    Owner = c.String(maxLength: 256),
                    AwardCriteria = c.String(maxLength: 32),
                    DateSynced = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Guid, unique: true);

            CreateTable(
                "dbo.Cancellations",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    TenderId = c.Int(nullable: false),
                    LotId = c.Int(),
                    Reason = c.String(),
                    Status = c.String(maxLength: 32),
                    Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    CancellationType = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lots", t => t.LotId)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.LotId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.CancellationDocuments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    CancellationId = c.Int(nullable: false),
                    RelatedItem = c.String(),
                    Url = c.String(),
                    DatePublished = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DocumentType = c.String(maxLength: 32),
                    Title = c.String(),
                    Description = c.String(),
                    Format = c.String(maxLength: 128),
                    Language = c.String(maxLength: 32),
                    DocumentOf = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cancellations", t => t.CancellationId, cascadeDelete: true)
                .Index(t => t.CancellationId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.Contracts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    TenderId = c.Int(nullable: false),
                    AwardId = c.Int(nullable: false),
                    Identifier = c.String(),
                    ContractNumber = c.String(),
                    Title = c.String(),
                    Description = c.String(),
                    Value_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Value_Currency = c.String(nullable: false, maxLength: 3),
                    Value_VATIncluded = c.Boolean(nullable: false),
                    Status = c.String(maxLength: 32),
                    Period_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    Period_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    DateSigned = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Awards", t => t.AwardId, cascadeDelete: true)
                .ForeignKey("dbo.Tenders", t => t.TenderId)
                .Index(t => t.TenderId)
                .Index(t => t.AwardId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.ContractDocuments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    ContractId = c.Int(nullable: false),
                    RelatedItem = c.String(),
                    Url = c.String(),
                    DatePublished = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DocumentType = c.String(maxLength: 32),
                    Title = c.String(),
                    Description = c.String(),
                    Format = c.String(maxLength: 128),
                    Language = c.String(maxLength: 32),
                    DocumentOf = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.ContractId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.Items",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    TenderId = c.Int(nullable: false),
                    LotId = c.Int(),
                    AwardId = c.Int(),
                    ContractId = c.Int(),
                    CPV_Id = c.String(maxLength: 32),
                    CPV_Description = c.String(),
                    Description = c.String(),
                    Unit_Code = c.String(maxLength: 32),
                    Unit_Name = c.String(maxLength: 128),
                    Quantity = c.Long(nullable: false),
                    DeliveryDate_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    DeliveryDate_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    DeliveryAddress_StreetAddress = c.String(),
                    DeliveryAddress_Locality = c.String(maxLength: 128),
                    DeliveryAddress_Region = c.String(maxLength: 128),
                    DeliveryAddress_PostalCode = c.String(maxLength: 32),
                    DeliveryAddress_CountryName = c.String(maxLength: 128),
                    DeliveryLocation_Latitude = c.String(maxLength: 32),
                    DeliveryLocation_Longitude = c.String(maxLength: 32),
                    DeliveryLocation_Elevation = c.String(maxLength: 32),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Awards", t => t.AwardId)
                .ForeignKey("dbo.Contracts", t => t.ContractId)
                .ForeignKey("dbo.Lots", t => t.LotId)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.LotId)
                .Index(t => t.AwardId)
                .Index(t => t.ContractId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.Classifications",
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
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);

            CreateTable(
                "dbo.ContractSuppliers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ContractId = c.Int(nullable: false),
                    Name = c.String(maxLength: 256),
                    Address_StreetAddress = c.String(),
                    Address_Locality = c.String(maxLength: 128),
                    Address_Region = c.String(maxLength: 128),
                    Address_PostalCode = c.String(maxLength: 32),
                    Address_CountryName = c.String(nullable: false, maxLength: 128),
                    СontactPoint_Name = c.String(nullable: false, maxLength: 256),
                    СontactPoint_Email = c.String(maxLength: 256),
                    СontactPoint_Telephone = c.String(maxLength: 256),
                    СontactPoint_FaxNumber = c.String(maxLength: 256),
                    СontactPoint_Url = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.ContractId);

            CreateTable(
                "dbo.ContractSupplierIdentifiers",
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
                .ForeignKey("dbo.ContractSuppliers", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);

            CreateTable(
                "dbo.TenderDocuments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    TenderId = c.Int(nullable: false),
                    RelatedItem = c.String(),
                    Url = c.String(),
                    DatePublished = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DocumentType = c.String(maxLength: 32),
                    Title = c.String(),
                    Description = c.String(),
                    Format = c.String(maxLength: 128),
                    Language = c.String(maxLength: 32),
                    DocumentOf = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.Features",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenderId = c.Int(nullable: false),
                    RelatedItem = c.String(),
                    Code = c.String(nullable: false, maxLength: 128),
                    FeatureType = c.Int(nullable: false),
                    Title = c.String(nullable: false),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId);

            CreateTable(
                "dbo.FeatureValues",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FeatureId = c.Int(nullable: false),
                    Value = c.Single(nullable: false),
                    Title = c.String(nullable: false),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.FeatureId, cascadeDelete: true)
                .Index(t => t.FeatureId);

            CreateTable(
                "dbo.ProcuringEntities",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Kind = c.Int(),
                    Name = c.String(maxLength: 256),
                    Address_StreetAddress = c.String(),
                    Address_Locality = c.String(maxLength: 128),
                    Address_Region = c.String(maxLength: 128),
                    Address_PostalCode = c.String(maxLength: 32),
                    Address_CountryName = c.String(nullable: false, maxLength: 128),
                    СontactPoint_Name = c.String(nullable: false, maxLength: 256),
                    СontactPoint_Email = c.String(maxLength: 256),
                    СontactPoint_Telephone = c.String(maxLength: 256),
                    СontactPoint_FaxNumber = c.String(maxLength: 256),
                    СontactPoint_Url = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenders", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.ProcuringEntityIdentifiers",
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
                .ForeignKey("dbo.ProcuringEntities", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);

            CreateTable(
                "dbo.Questions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    TenderId = c.Int(nullable: false),
                    RelatedItem = c.String(),
                    Title = c.String(nullable: false),
                    Description = c.String(),
                    Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Answer = c.String(),
                    QuestionOf = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.QuestionAuthors",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Name = c.String(maxLength: 256),
                    Address_StreetAddress = c.String(),
                    Address_Locality = c.String(maxLength: 128),
                    Address_Region = c.String(maxLength: 128),
                    Address_PostalCode = c.String(maxLength: 32),
                    Address_CountryName = c.String(nullable: false, maxLength: 128),
                    СontactPoint_Name = c.String(nullable: false, maxLength: 256),
                    СontactPoint_Email = c.String(maxLength: 256),
                    СontactPoint_Telephone = c.String(maxLength: 256),
                    СontactPoint_FaxNumber = c.String(maxLength: 256),
                    СontactPoint_Url = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.QuestionAuthorIdentifiers",
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
                .ForeignKey("dbo.QuestionAuthors", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);

            CreateTable(
                "dbo.Revisions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenderId = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId);

            CreateTable(
                "dbo.Changes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    RevisionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Revisions", t => t.RevisionId, cascadeDelete: true)
                .Index(t => t.RevisionId);

            CreateTable(
                "dbo.Parameters",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    BidId = c.Int(nullable: false),
                    Code = c.String(nullable: false, maxLength: 128),
                    Value = c.Single(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bids", t => t.BidId, cascadeDelete: true)
                .Index(t => t.BidId);

            CreateTable(
                "dbo.Tenderers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    BidId = c.Int(nullable: false),
                    Name = c.String(maxLength: 256),
                    Address_StreetAddress = c.String(),
                    Address_Locality = c.String(maxLength: 128),
                    Address_Region = c.String(maxLength: 128),
                    Address_PostalCode = c.String(maxLength: 32),
                    Address_CountryName = c.String(nullable: false, maxLength: 128),
                    СontactPoint_Name = c.String(nullable: false, maxLength: 256),
                    СontactPoint_Email = c.String(maxLength: 256),
                    СontactPoint_Telephone = c.String(maxLength: 256),
                    СontactPoint_FaxNumber = c.String(maxLength: 256),
                    СontactPoint_Url = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bids", t => t.BidId, cascadeDelete: true)
                .Index(t => t.BidId);

            CreateTable(
                "dbo.TendererIdentifiers",
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
                .ForeignKey("dbo.Tenderers", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);

            CreateTable(
                "dbo.AwardDocuments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    AwardId = c.Int(nullable: false),
                    RelatedItem = c.String(),
                    Url = c.String(),
                    DatePublished = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DocumentType = c.String(maxLength: 32),
                    Title = c.String(),
                    Description = c.String(),
                    Format = c.String(maxLength: 128),
                    Language = c.String(maxLength: 32),
                    DocumentOf = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Awards", t => t.AwardId, cascadeDelete: true)
                .Index(t => t.AwardId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.Suppliers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AwardId = c.Int(nullable: false),
                    Name = c.String(maxLength: 256),
                    Address_StreetAddress = c.String(),
                    Address_Locality = c.String(maxLength: 128),
                    Address_Region = c.String(maxLength: 128),
                    Address_PostalCode = c.String(maxLength: 32),
                    Address_CountryName = c.String(nullable: false, maxLength: 128),
                    СontactPoint_Name = c.String(nullable: false, maxLength: 256),
                    СontactPoint_Email = c.String(maxLength: 256),
                    СontactPoint_Telephone = c.String(maxLength: 256),
                    СontactPoint_FaxNumber = c.String(maxLength: 256),
                    СontactPoint_Url = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Awards", t => t.AwardId, cascadeDelete: true)
                .Index(t => t.AwardId);

            CreateTable(
                "dbo.SupplierIdentifiers",
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
                .ForeignKey("dbo.Suppliers", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);

            CreateTable(
                "dbo.AwardComplaintDocuments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    ComplaintId = c.Int(nullable: false),
                    RelatedItem = c.String(),
                    Url = c.String(),
                    DatePublished = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    DocumentType = c.String(maxLength: 32),
                    Title = c.String(),
                    Description = c.String(),
                    Format = c.String(maxLength: 128),
                    Language = c.String(maxLength: 32),
                    DocumentOf = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AwardComplaints", t => t.ComplaintId, cascadeDelete: true)
                .Index(t => t.ComplaintId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.ClassificationSchemes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Scheme = c.String(nullable: false, maxLength: 16),
                    Name = c.String(nullable: false, maxLength: 256),
                    Description = c.String(),
                    Category = c.String(nullable: false, maxLength: 2),
                    Url = c.String(),
                    Public = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Scheme, unique: true);

            CreateTable(
                "dbo.CPV",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 10),
                    Description_Uk = c.String(),
                    Description_En = c.String(),
                    Description_Ru = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.GSIN",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 14),
                    Description_Uk = c.String(),
                    Description_En = c.String(),
                    Description_Ru = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.CreatedTenders",
                c => new
                {
                    Id = c.Int(nullable: false),
                    UserOrganizationId = c.Int(nullable: false),
                    Token = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenders", t => t.Id)
                .ForeignKey("dbo.UserOrganizations", t => t.UserOrganizationId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.UserOrganizationId);

            CreateTable(
                "dbo.UserOrganizations",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Kind = c.Int(),
                    Name = c.String(maxLength: 256),
                    Address_StreetAddress = c.String(),
                    Address_Locality = c.String(maxLength: 128),
                    Address_Region = c.String(maxLength: 128),
                    Address_PostalCode = c.String(maxLength: 32),
                    Address_CountryName = c.String(nullable: false, maxLength: 128),
                    СontactPoint_Name = c.String(nullable: false, maxLength: 256),
                    СontactPoint_Email = c.String(maxLength: 256),
                    СontactPoint_Telephone = c.String(maxLength: 256),
                    СontactPoint_FaxNumber = c.String(maxLength: 256),
                    СontactPoint_Url = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserOrganizationIdentifiers",
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
                .ForeignKey("dbo.UserOrganizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);

            CreateTable(
                "dbo.DraftClassifications",
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
                .ForeignKey("dbo.DraftItems", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);

            CreateTable(
                "dbo.DraftItems",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    TenderId = c.Int(nullable: false),
                    LotId = c.Int(),
                    Classification_Id = c.String(maxLength: 32),
                    Classification_Description = c.String(),
                    Description = c.String(),
                    Unit_Code = c.String(maxLength: 32),
                    Unit_Name = c.String(maxLength: 128),
                    Quantity = c.Long(nullable: false),
                    DeliveryDate_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    DeliveryDate_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    DeliveryAddress_StreetAddress = c.String(),
                    DeliveryAddress_Locality = c.String(maxLength: 128),
                    DeliveryAddress_Region = c.String(maxLength: 128),
                    DeliveryAddress_PostalCode = c.String(maxLength: 32),
                    DeliveryAddress_CountryName = c.String(maxLength: 128),
                    DeliveryLocation_Latitude = c.String(maxLength: 32),
                    DeliveryLocation_Longitude = c.String(maxLength: 32),
                    DeliveryLocation_Elevation = c.String(maxLength: 32),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DraftLots", t => t.LotId)
                .ForeignKey("dbo.DraftTenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.LotId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.DraftLots",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    TenderId = c.Int(nullable: false),
                    Title = c.String(),
                    Description = c.String(),
                    GuaranteeOptional_Amount = c.Decimal(precision: 18, scale: 2),
                    GuaranteeOptional_Currency = c.String(maxLength: 3),
                    Value_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Value_Currency = c.String(nullable: false, maxLength: 3),
                    Value_VATIncluded = c.Boolean(nullable: false),
                    MinimalStep_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    MinimalStep_Currency = c.String(nullable: false, maxLength: 3),
                    MinimalStep_VATIncluded = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DraftTenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.DraftTenders",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Guid = c.Guid(nullable: false),
                    ProcuringEntityId = c.Int(nullable: false),
                    Title = c.String(),
                    Description = c.String(),
                    DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    GuaranteeOptional_Amount = c.Decimal(precision: 18, scale: 2),
                    GuaranteeOptional_Currency = c.String(maxLength: 3),
                    Value_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Value_Currency = c.String(nullable: false, maxLength: 3),
                    Value_VATIncluded = c.Boolean(nullable: false),
                    MinimalStep_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    MinimalStep_Currency = c.String(nullable: false, maxLength: 3),
                    MinimalStep_VATIncluded = c.Boolean(nullable: false),
                    EnquiryPeriod_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    EnquiryPeriod_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    TenderPeriod_StartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    TenderPeriod_EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserOrganizations", t => t.ProcuringEntityId, cascadeDelete: true)
                .Index(t => t.ProcuringEntityId)
                .Index(t => t.Guid);

            CreateTable(
                "dbo.DraftTenderDocuments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StringId = c.String(nullable: false, maxLength: 64),
                    TenderId = c.Int(nullable: false),
                    Data = c.Binary(),
                    DocumentType = c.String(maxLength: 32),
                    Title = c.String(),
                    Description = c.String(),
                    Format = c.String(maxLength: 128),
                    Language = c.String(maxLength: 32),
                    DocumentOf = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DraftTenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId)
                .Index(t => t.StringId);

            CreateTable(
                "dbo.DraftFeatures",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenderId = c.Int(nullable: false),
                    RelatedId = c.Int(),
                    FeatureType = c.Int(nullable: false),
                    Title = c.String(nullable: false),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DraftTenders", t => t.TenderId, cascadeDelete: true)
                .Index(t => t.TenderId);

            CreateTable(
                "dbo.DraftFeatureValues",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FeatureId = c.Int(nullable: false),
                    Value = c.Single(nullable: false),
                    Title = c.String(nullable: false),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DraftFeatures", t => t.FeatureId, cascadeDelete: true)
                .Index(t => t.FeatureId);

            CreateTable(
                "dbo.SyncErrors",
                c => new
                {
                    TenderGuid = c.Guid(nullable: false),
                    Offset = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.TenderGuid);

            CreateTable(
                "dbo.Units",
                c => new
                {
                    Code = c.String(nullable: false, maxLength: 3),
                    Name = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Code);
        }

        public override void Down()
        {
            DropForeignKey("dbo.DraftTenders", "ProcuringEntityId", "dbo.UserOrganizations");
            DropForeignKey("dbo.DraftLots", "TenderId", "dbo.DraftTenders");
            DropForeignKey("dbo.DraftItems", "TenderId", "dbo.DraftTenders");
            DropForeignKey("dbo.DraftFeatureValues", "FeatureId", "dbo.DraftFeatures");
            DropForeignKey("dbo.DraftFeatures", "TenderId", "dbo.DraftTenders");
            DropForeignKey("dbo.DraftTenderDocuments", "TenderId", "dbo.DraftTenders");
            DropForeignKey("dbo.DraftItems", "LotId", "dbo.DraftLots");
            DropForeignKey("dbo.DraftClassifications", "ItemId", "dbo.DraftItems");
            DropForeignKey("dbo.CreatedTenders", "UserOrganizationId", "dbo.UserOrganizations");
            DropForeignKey("dbo.UserOrganizationIdentifiers", "OrganizationId", "dbo.UserOrganizations");
            DropForeignKey("dbo.CreatedTenders", "Id", "dbo.Tenders");
            DropForeignKey("dbo.AwardComplaints", "LotId", "dbo.Lots");
            DropForeignKey("dbo.AwardComplaintDocuments", "ComplaintId", "dbo.AwardComplaints");
            DropForeignKey("dbo.Suppliers", "AwardId", "dbo.Awards");
            DropForeignKey("dbo.SupplierIdentifiers", "OrganizationId", "dbo.Suppliers");
            DropForeignKey("dbo.Awards", "LotId", "dbo.Lots");
            DropForeignKey("dbo.AwardDocuments", "AwardId", "dbo.Awards");
            DropForeignKey("dbo.AwardComplaints", "AwardId", "dbo.Awards");
            DropForeignKey("dbo.Awards", "BidId", "dbo.Bids");
            DropForeignKey("dbo.Tenderers", "BidId", "dbo.Bids");
            DropForeignKey("dbo.TendererIdentifiers", "OrganizationId", "dbo.Tenderers");
            DropForeignKey("dbo.Parameters", "BidId", "dbo.Bids");
            DropForeignKey("dbo.LotValues", "LotId", "dbo.Lots");
            DropForeignKey("dbo.Revisions", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.Changes", "RevisionId", "dbo.Revisions");
            DropForeignKey("dbo.Questions", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.QuestionAuthors", "Id", "dbo.Questions");
            DropForeignKey("dbo.QuestionAuthorIdentifiers", "OrganizationId", "dbo.QuestionAuthors");
            DropForeignKey("dbo.ProcuringEntities", "Id", "dbo.Tenders");
            DropForeignKey("dbo.ProcuringEntityIdentifiers", "OrganizationId", "dbo.ProcuringEntities");
            DropForeignKey("dbo.Lots", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.FeatureValues", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.Features", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.TenderDocuments", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.Contracts", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.ContractSuppliers", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.ContractSupplierIdentifiers", "OrganizationId", "dbo.ContractSuppliers");
            DropForeignKey("dbo.Items", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.Items", "LotId", "dbo.Lots");
            DropForeignKey("dbo.Items", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Items", "AwardId", "dbo.Awards");
            DropForeignKey("dbo.Classifications", "ItemId", "dbo.Items");
            DropForeignKey("dbo.ContractDocuments", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Contracts", "AwardId", "dbo.Awards");
            DropForeignKey("dbo.TenderComplaints", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.Cancellations", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.Cancellations", "LotId", "dbo.Lots");
            DropForeignKey("dbo.CancellationDocuments", "CancellationId", "dbo.Cancellations");
            DropForeignKey("dbo.Bids", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.Awards", "TenderId", "dbo.Tenders");
            DropForeignKey("dbo.TenderComplaints", "LotId", "dbo.Lots");
            DropForeignKey("dbo.TenderComplaintDocuments", "ComplaintId", "dbo.TenderComplaints");
            DropForeignKey("dbo.TenderComplaintAuthors", "Id", "dbo.TenderComplaints");
            DropForeignKey("dbo.TenderComplaintAuthorIdentifiers", "OrganizationId", "dbo.TenderComplaintAuthors");
            DropForeignKey("dbo.LotValues", "BidId", "dbo.Bids");
            DropForeignKey("dbo.BidDocuments", "BidId", "dbo.Bids");
            DropForeignKey("dbo.AwardComplaintAuthors", "Id", "dbo.AwardComplaints");
            DropForeignKey("dbo.AwardComplaintAuthorIdentifiers", "OrganizationId", "dbo.AwardComplaintAuthors");
            DropIndex("dbo.DraftFeatureValues", new[] { "FeatureId" });
            DropIndex("dbo.DraftFeatures", new[] { "TenderId" });
            DropIndex("dbo.DraftTenderDocuments", new[] { "StringId" });
            DropIndex("dbo.DraftTenderDocuments", new[] { "TenderId" });
            DropIndex("dbo.DraftTenders", new[] { "Guid" });
            DropIndex("dbo.DraftTenders", new[] { "ProcuringEntityId" });
            DropIndex("dbo.DraftLots", new[] { "StringId" });
            DropIndex("dbo.DraftLots", new[] { "TenderId" });
            DropIndex("dbo.DraftItems", new[] { "StringId" });
            DropIndex("dbo.DraftItems", new[] { "LotId" });
            DropIndex("dbo.DraftItems", new[] { "TenderId" });
            DropIndex("dbo.DraftClassifications", new[] { "ItemId" });
            DropIndex("dbo.UserOrganizationIdentifiers", new[] { "OrganizationId" });
            DropIndex("dbo.CreatedTenders", new[] { "UserOrganizationId" });
            DropIndex("dbo.CreatedTenders", new[] { "Id" });
            DropIndex("dbo.ClassificationSchemes", new[] { "Scheme" });
            DropIndex("dbo.AwardComplaintDocuments", new[] { "StringId" });
            DropIndex("dbo.AwardComplaintDocuments", new[] { "ComplaintId" });
            DropIndex("dbo.SupplierIdentifiers", new[] { "OrganizationId" });
            DropIndex("dbo.Suppliers", new[] { "AwardId" });
            DropIndex("dbo.AwardDocuments", new[] { "StringId" });
            DropIndex("dbo.AwardDocuments", new[] { "AwardId" });
            DropIndex("dbo.TendererIdentifiers", new[] { "OrganizationId" });
            DropIndex("dbo.Tenderers", new[] { "BidId" });
            DropIndex("dbo.Parameters", new[] { "BidId" });
            DropIndex("dbo.Changes", new[] { "RevisionId" });
            DropIndex("dbo.Revisions", new[] { "TenderId" });
            DropIndex("dbo.QuestionAuthorIdentifiers", new[] { "OrganizationId" });
            DropIndex("dbo.QuestionAuthors", new[] { "Id" });
            DropIndex("dbo.Questions", new[] { "StringId" });
            DropIndex("dbo.Questions", new[] { "TenderId" });
            DropIndex("dbo.ProcuringEntityIdentifiers", new[] { "OrganizationId" });
            DropIndex("dbo.ProcuringEntities", new[] { "Id" });
            DropIndex("dbo.FeatureValues", new[] { "FeatureId" });
            DropIndex("dbo.Features", new[] { "TenderId" });
            DropIndex("dbo.TenderDocuments", new[] { "StringId" });
            DropIndex("dbo.TenderDocuments", new[] { "TenderId" });
            DropIndex("dbo.ContractSupplierIdentifiers", new[] { "OrganizationId" });
            DropIndex("dbo.ContractSuppliers", new[] { "ContractId" });
            DropIndex("dbo.Classifications", new[] { "ItemId" });
            DropIndex("dbo.Items", new[] { "StringId" });
            DropIndex("dbo.Items", new[] { "ContractId" });
            DropIndex("dbo.Items", new[] { "AwardId" });
            DropIndex("dbo.Items", new[] { "LotId" });
            DropIndex("dbo.Items", new[] { "TenderId" });
            DropIndex("dbo.ContractDocuments", new[] { "StringId" });
            DropIndex("dbo.ContractDocuments", new[] { "ContractId" });
            DropIndex("dbo.Contracts", new[] { "StringId" });
            DropIndex("dbo.Contracts", new[] { "AwardId" });
            DropIndex("dbo.Contracts", new[] { "TenderId" });
            DropIndex("dbo.CancellationDocuments", new[] { "StringId" });
            DropIndex("dbo.CancellationDocuments", new[] { "CancellationId" });
            DropIndex("dbo.Cancellations", new[] { "StringId" });
            DropIndex("dbo.Cancellations", new[] { "LotId" });
            DropIndex("dbo.Cancellations", new[] { "TenderId" });
            DropIndex("dbo.Tenders", new[] { "Guid" });
            DropIndex("dbo.TenderComplaintDocuments", new[] { "StringId" });
            DropIndex("dbo.TenderComplaintDocuments", new[] { "ComplaintId" });
            DropIndex("dbo.TenderComplaintAuthorIdentifiers", new[] { "OrganizationId" });
            DropIndex("dbo.TenderComplaintAuthors", new[] { "Id" });
            DropIndex("dbo.TenderComplaints", new[] { "StringId" });
            DropIndex("dbo.TenderComplaints", new[] { "LotId" });
            DropIndex("dbo.TenderComplaints", new[] { "TenderId" });
            DropIndex("dbo.Lots", new[] { "StringId" });
            DropIndex("dbo.Lots", new[] { "TenderId" });
            DropIndex("dbo.LotValues", new[] { "LotId" });
            DropIndex("dbo.LotValues", new[] { "BidId" });
            DropIndex("dbo.BidDocuments", new[] { "StringId" });
            DropIndex("dbo.BidDocuments", new[] { "BidId" });
            DropIndex("dbo.Bids", new[] { "StringId" });
            DropIndex("dbo.Bids", new[] { "TenderId" });
            DropIndex("dbo.Awards", new[] { "StringId" });
            DropIndex("dbo.Awards", new[] { "LotId" });
            DropIndex("dbo.Awards", new[] { "BidId" });
            DropIndex("dbo.Awards", new[] { "TenderId" });
            DropIndex("dbo.AwardComplaints", new[] { "StringId" });
            DropIndex("dbo.AwardComplaints", new[] { "LotId" });
            DropIndex("dbo.AwardComplaints", new[] { "AwardId" });
            DropIndex("dbo.AwardComplaintAuthors", new[] { "Id" });
            DropIndex("dbo.AwardComplaintAuthorIdentifiers", new[] { "OrganizationId" });
            DropTable("dbo.Units");
            DropTable("dbo.SyncErrors");
            DropTable("dbo.DraftFeatureValues");
            DropTable("dbo.DraftFeatures");
            DropTable("dbo.DraftTenderDocuments");
            DropTable("dbo.DraftTenders");
            DropTable("dbo.DraftLots");
            DropTable("dbo.DraftItems");
            DropTable("dbo.DraftClassifications");
            DropTable("dbo.UserOrganizationIdentifiers");
            DropTable("dbo.UserOrganizations");
            DropTable("dbo.CreatedTenders");
            DropTable("dbo.GSIN");
            DropTable("dbo.CPV");
            DropTable("dbo.ClassificationSchemes");
            DropTable("dbo.AwardComplaintDocuments");
            DropTable("dbo.SupplierIdentifiers");
            DropTable("dbo.Suppliers");
            DropTable("dbo.AwardDocuments");
            DropTable("dbo.TendererIdentifiers");
            DropTable("dbo.Tenderers");
            DropTable("dbo.Parameters");
            DropTable("dbo.Changes");
            DropTable("dbo.Revisions");
            DropTable("dbo.QuestionAuthorIdentifiers");
            DropTable("dbo.QuestionAuthors");
            DropTable("dbo.Questions");
            DropTable("dbo.ProcuringEntityIdentifiers");
            DropTable("dbo.ProcuringEntities");
            DropTable("dbo.FeatureValues");
            DropTable("dbo.Features");
            DropTable("dbo.TenderDocuments");
            DropTable("dbo.ContractSupplierIdentifiers");
            DropTable("dbo.ContractSuppliers");
            DropTable("dbo.Classifications");
            DropTable("dbo.Items");
            DropTable("dbo.ContractDocuments");
            DropTable("dbo.Contracts");
            DropTable("dbo.CancellationDocuments");
            DropTable("dbo.Cancellations");
            DropTable("dbo.Tenders");
            DropTable("dbo.TenderComplaintDocuments");
            DropTable("dbo.TenderComplaintAuthorIdentifiers");
            DropTable("dbo.TenderComplaintAuthors");
            DropTable("dbo.TenderComplaints");
            DropTable("dbo.Lots");
            DropTable("dbo.LotValues");
            DropTable("dbo.BidDocuments");
            DropTable("dbo.Bids");
            DropTable("dbo.Awards");
            DropTable("dbo.AwardComplaints");
            DropTable("dbo.AwardComplaintAuthors");
            DropTable("dbo.AwardComplaintAuthorIdentifiers");
        }
    }
}
