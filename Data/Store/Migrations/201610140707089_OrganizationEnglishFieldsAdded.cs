namespace Kapitalist.Data.Store.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OrganizationEnglishFieldsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AwardComplaintAuthorIdentifiers", "LegalNameEn", c => c.String());
            AddColumn("dbo.AwardComplaintAuthors", "NameEn", c => c.String());
            AddColumn("dbo.TenderComplaintAuthors", "NameEn", c => c.String());
            AddColumn("dbo.TenderComplaintAuthorIdentifiers", "LegalNameEn", c => c.String());
            AddColumn("dbo.ContractSuppliers", "NameEn", c => c.String());
            AddColumn("dbo.ContractSupplierIdentifiers", "LegalNameEn", c => c.String());
            AddColumn("dbo.ProcuringEntities", "NameEn", c => c.String());
            AddColumn("dbo.ProcuringEntityIdentifiers", "LegalNameEn", c => c.String());
            AddColumn("dbo.QuestionAuthors", "NameEn", c => c.String());
            AddColumn("dbo.QuestionAuthorIdentifiers", "LegalNameEn", c => c.String());
            AddColumn("dbo.Tenderers", "NameEn", c => c.String());
            AddColumn("dbo.TendererIdentifiers", "LegalNameEn", c => c.String());
            AddColumn("dbo.Suppliers", "NameEn", c => c.String());
            AddColumn("dbo.SupplierIdentifiers", "LegalNameEn", c => c.String());
            AddColumn("dbo.UserOrganizations", "NameEn", c => c.String());
            AddColumn("dbo.UserOrganizationIdentifiers", "LegalNameEn", c => c.String());
            AddColumn("dbo.PlanProcuringEntities", "NameEn", c => c.String());
            AddColumn("dbo.PlanProcuringEntityIdentifiers", "LegalNameEn", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.PlanProcuringEntityIdentifiers", "LegalNameEn");
            DropColumn("dbo.PlanProcuringEntities", "NameEn");
            DropColumn("dbo.UserOrganizationIdentifiers", "LegalNameEn");
            DropColumn("dbo.UserOrganizations", "NameEn");
            DropColumn("dbo.SupplierIdentifiers", "LegalNameEn");
            DropColumn("dbo.Suppliers", "NameEn");
            DropColumn("dbo.TendererIdentifiers", "LegalNameEn");
            DropColumn("dbo.Tenderers", "NameEn");
            DropColumn("dbo.QuestionAuthorIdentifiers", "LegalNameEn");
            DropColumn("dbo.QuestionAuthors", "NameEn");
            DropColumn("dbo.ProcuringEntityIdentifiers", "LegalNameEn");
            DropColumn("dbo.ProcuringEntities", "NameEn");
            DropColumn("dbo.ContractSupplierIdentifiers", "LegalNameEn");
            DropColumn("dbo.ContractSuppliers", "NameEn");
            DropColumn("dbo.TenderComplaintAuthorIdentifiers", "LegalNameEn");
            DropColumn("dbo.TenderComplaintAuthors", "NameEn");
            DropColumn("dbo.AwardComplaintAuthors", "NameEn");
            DropColumn("dbo.AwardComplaintAuthorIdentifiers", "LegalNameEn");
        }
    }
}