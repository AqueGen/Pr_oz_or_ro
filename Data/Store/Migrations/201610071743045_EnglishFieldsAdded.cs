namespace Kapitalist.Data.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnglishFieldsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AwardComplaintAuthors", "ContactPoint_NameEn", c => c.String());
            AddColumn("dbo.AwardComplaintAuthors", "ContactPoint_AvailableLanguage", c => c.String());
            AddColumn("dbo.Lots", "TitleEn", c => c.String());
            AddColumn("dbo.Lots", "DescriptionEn", c => c.String());
            AddColumn("dbo.TenderComplaintAuthors", "ContactPoint_NameEn", c => c.String());
            AddColumn("dbo.TenderComplaintAuthors", "ContactPoint_AvailableLanguage", c => c.String());
            AddColumn("dbo.Tenders", "DescriptionEn", c => c.String());
            AddColumn("dbo.Items", "DescriptionEn", c => c.String());
            AddColumn("dbo.ContractSuppliers", "ContactPoint_NameEn", c => c.String());
            AddColumn("dbo.ContractSuppliers", "ContactPoint_AvailableLanguage", c => c.String());
            AddColumn("dbo.Features", "TitleEn", c => c.String());
            AddColumn("dbo.Features", "DescriptionEn", c => c.String());
            AddColumn("dbo.FeatureValues", "TitleEn", c => c.String());
            AddColumn("dbo.ProcuringEntities", "ContactPoint_NameEn", c => c.String());
            AddColumn("dbo.ProcuringEntities", "ContactPoint_AvailableLanguage", c => c.String());
            AddColumn("dbo.QuestionAuthors", "ContactPoint_NameEn", c => c.String());
            AddColumn("dbo.QuestionAuthors", "ContactPoint_AvailableLanguage", c => c.String());
            AddColumn("dbo.Tenderers", "ContactPoint_NameEn", c => c.String());
            AddColumn("dbo.Tenderers", "ContactPoint_AvailableLanguage", c => c.String());
            AddColumn("dbo.Suppliers", "ContactPoint_NameEn", c => c.String());
            AddColumn("dbo.Suppliers", "ContactPoint_AvailableLanguage", c => c.String());
            AddColumn("dbo.UserOrganizations", "ContactPoint_NameEn", c => c.String());
            AddColumn("dbo.UserOrganizations", "ContactPoint_AvailableLanguage", c => c.String());
            AddColumn("dbo.DraftItems", "DescriptionEn", c => c.String());
            AddColumn("dbo.DraftLots", "TitleEn", c => c.String());
            AddColumn("dbo.DraftLots", "DescriptionEn", c => c.String());
            AddColumn("dbo.DraftTenders", "DescriptionEn", c => c.String());
            AddColumn("dbo.DraftFeatures", "TitleEn", c => c.String());
            AddColumn("dbo.DraftFeatures", "DescriptionEn", c => c.String());
            AddColumn("dbo.DraftFeatureValues", "TitleEn", c => c.String());
            AddColumn("dbo.DraftPlanItems", "DescriptionEn", c => c.String());
            AddColumn("dbo.PlanItems", "DescriptionEn", c => c.String());
            AddColumn("dbo.PlanProcuringEntities", "ContactPointOptional_NameEn", c => c.String());
            AddColumn("dbo.PlanProcuringEntities", "ContactPointOptional_AvailableLanguage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlanProcuringEntities", "ContactPointOptional_AvailableLanguage");
            DropColumn("dbo.PlanProcuringEntities", "ContactPointOptional_NameEn");
            DropColumn("dbo.PlanItems", "DescriptionEn");
            DropColumn("dbo.DraftPlanItems", "DescriptionEn");
            DropColumn("dbo.DraftFeatureValues", "TitleEn");
            DropColumn("dbo.DraftFeatures", "DescriptionEn");
            DropColumn("dbo.DraftFeatures", "TitleEn");
            DropColumn("dbo.DraftTenders", "DescriptionEn");
            DropColumn("dbo.DraftLots", "DescriptionEn");
            DropColumn("dbo.DraftLots", "TitleEn");
            DropColumn("dbo.DraftItems", "DescriptionEn");
            DropColumn("dbo.UserOrganizations", "ContactPoint_AvailableLanguage");
            DropColumn("dbo.UserOrganizations", "ContactPoint_NameEn");
            DropColumn("dbo.Suppliers", "ContactPoint_AvailableLanguage");
            DropColumn("dbo.Suppliers", "ContactPoint_NameEn");
            DropColumn("dbo.Tenderers", "ContactPoint_AvailableLanguage");
            DropColumn("dbo.Tenderers", "ContactPoint_NameEn");
            DropColumn("dbo.QuestionAuthors", "ContactPoint_AvailableLanguage");
            DropColumn("dbo.QuestionAuthors", "ContactPoint_NameEn");
            DropColumn("dbo.ProcuringEntities", "ContactPoint_AvailableLanguage");
            DropColumn("dbo.ProcuringEntities", "ContactPoint_NameEn");
            DropColumn("dbo.FeatureValues", "TitleEn");
            DropColumn("dbo.Features", "DescriptionEn");
            DropColumn("dbo.Features", "TitleEn");
            DropColumn("dbo.ContractSuppliers", "ContactPoint_AvailableLanguage");
            DropColumn("dbo.ContractSuppliers", "ContactPoint_NameEn");
            DropColumn("dbo.Items", "DescriptionEn");
            DropColumn("dbo.Tenders", "DescriptionEn");
            DropColumn("dbo.TenderComplaintAuthors", "ContactPoint_AvailableLanguage");
            DropColumn("dbo.TenderComplaintAuthors", "ContactPoint_NameEn");
            DropColumn("dbo.Lots", "DescriptionEn");
            DropColumn("dbo.Lots", "TitleEn");
            DropColumn("dbo.AwardComplaintAuthors", "ContactPoint_AvailableLanguage");
            DropColumn("dbo.AwardComplaintAuthors", "ContactPoint_NameEn");
        }
    }
}
