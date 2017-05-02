namespace Kapitalist.Data.Store.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OrganizationContactPointFixed : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PlanProcuringEntities", name: "СontactPoint_Email", newName: "ContactPoint_Email");
            RenameColumn(table: "dbo.PlanProcuringEntities", name: "СontactPoint_Telephone", newName: "ContactPoint_Telephone");
            RenameColumn(table: "dbo.PlanProcuringEntities", name: "СontactPoint_FaxNumber", newName: "ContactPoint_FaxNumber");
            RenameColumn(table: "dbo.PlanProcuringEntities", name: "СontactPoint_Url", newName: "ContactPoint_Url");
            RenameColumn(table: "dbo.AwardComplaintAuthors", name: "СontactPoint_Name", newName: "ContactPoint_Name");
            RenameColumn(table: "dbo.AwardComplaintAuthors", name: "СontactPoint_Email", newName: "ContactPoint_Email");
            RenameColumn(table: "dbo.AwardComplaintAuthors", name: "СontactPoint_Telephone", newName: "ContactPoint_Telephone");
            RenameColumn(table: "dbo.AwardComplaintAuthors", name: "СontactPoint_FaxNumber", newName: "ContactPoint_FaxNumber");
            RenameColumn(table: "dbo.AwardComplaintAuthors", name: "СontactPoint_Url", newName: "ContactPoint_Url");
            RenameColumn(table: "dbo.TenderComplaintAuthors", name: "СontactPoint_Name", newName: "ContactPoint_Name");
            RenameColumn(table: "dbo.TenderComplaintAuthors", name: "СontactPoint_Email", newName: "ContactPoint_Email");
            RenameColumn(table: "dbo.TenderComplaintAuthors", name: "СontactPoint_Telephone", newName: "ContactPoint_Telephone");
            RenameColumn(table: "dbo.TenderComplaintAuthors", name: "СontactPoint_FaxNumber", newName: "ContactPoint_FaxNumber");
            RenameColumn(table: "dbo.TenderComplaintAuthors", name: "СontactPoint_Url", newName: "ContactPoint_Url");
            RenameColumn(table: "dbo.ContractSuppliers", name: "СontactPoint_Name", newName: "ContactPoint_Name");
            RenameColumn(table: "dbo.ContractSuppliers", name: "СontactPoint_Email", newName: "ContactPoint_Email");
            RenameColumn(table: "dbo.ContractSuppliers", name: "СontactPoint_Telephone", newName: "ContactPoint_Telephone");
            RenameColumn(table: "dbo.ContractSuppliers", name: "СontactPoint_FaxNumber", newName: "ContactPoint_FaxNumber");
            RenameColumn(table: "dbo.ContractSuppliers", name: "СontactPoint_Url", newName: "ContactPoint_Url");
            RenameColumn(table: "dbo.ProcuringEntities", name: "СontactPoint_Name", newName: "ContactPoint_Name");
            RenameColumn(table: "dbo.ProcuringEntities", name: "СontactPoint_Email", newName: "ContactPoint_Email");
            RenameColumn(table: "dbo.ProcuringEntities", name: "СontactPoint_Telephone", newName: "ContactPoint_Telephone");
            RenameColumn(table: "dbo.ProcuringEntities", name: "СontactPoint_FaxNumber", newName: "ContactPoint_FaxNumber");
            RenameColumn(table: "dbo.ProcuringEntities", name: "СontactPoint_Url", newName: "ContactPoint_Url");
            RenameColumn(table: "dbo.QuestionAuthors", name: "СontactPoint_Name", newName: "ContactPoint_Name");
            RenameColumn(table: "dbo.QuestionAuthors", name: "СontactPoint_Email", newName: "ContactPoint_Email");
            RenameColumn(table: "dbo.QuestionAuthors", name: "СontactPoint_Telephone", newName: "ContactPoint_Telephone");
            RenameColumn(table: "dbo.QuestionAuthors", name: "СontactPoint_FaxNumber", newName: "ContactPoint_FaxNumber");
            RenameColumn(table: "dbo.QuestionAuthors", name: "СontactPoint_Url", newName: "ContactPoint_Url");
            RenameColumn(table: "dbo.Tenderers", name: "СontactPoint_Name", newName: "ContactPoint_Name");
            RenameColumn(table: "dbo.Tenderers", name: "СontactPoint_Email", newName: "ContactPoint_Email");
            RenameColumn(table: "dbo.Tenderers", name: "СontactPoint_Telephone", newName: "ContactPoint_Telephone");
            RenameColumn(table: "dbo.Tenderers", name: "СontactPoint_FaxNumber", newName: "ContactPoint_FaxNumber");
            RenameColumn(table: "dbo.Tenderers", name: "СontactPoint_Url", newName: "ContactPoint_Url");
            RenameColumn(table: "dbo.Suppliers", name: "СontactPoint_Name", newName: "ContactPoint_Name");
            RenameColumn(table: "dbo.Suppliers", name: "СontactPoint_Email", newName: "ContactPoint_Email");
            RenameColumn(table: "dbo.Suppliers", name: "СontactPoint_Telephone", newName: "ContactPoint_Telephone");
            RenameColumn(table: "dbo.Suppliers", name: "СontactPoint_FaxNumber", newName: "ContactPoint_FaxNumber");
            RenameColumn(table: "dbo.Suppliers", name: "СontactPoint_Url", newName: "ContactPoint_Url");
            RenameColumn(table: "dbo.UserOrganizations", name: "СontactPoint_Name", newName: "ContactPoint_Name");
            RenameColumn(table: "dbo.UserOrganizations", name: "СontactPoint_Email", newName: "ContactPoint_Email");
            RenameColumn(table: "dbo.UserOrganizations", name: "СontactPoint_Telephone", newName: "ContactPoint_Telephone");
            RenameColumn(table: "dbo.UserOrganizations", name: "СontactPoint_FaxNumber", newName: "ContactPoint_FaxNumber");
            RenameColumn(table: "dbo.UserOrganizations", name: "СontactPoint_Url", newName: "ContactPoint_Url");
            AlterColumn("dbo.Tenders", "Status", c => c.String(maxLength: 36));
        }

        public override void Down()
        {
            RenameColumn(table: "dbo.PlanProcuringEntities", name: "ContactPoint_Email", newName: "СontactPoint_Email");
            RenameColumn(table: "dbo.PlanProcuringEntities", name: "ContactPoint_Telephone", newName: "СontactPoint_Telephone");
            RenameColumn(table: "dbo.PlanProcuringEntities", name: "ContactPoint_FaxNumber", newName: "СontactPoint_FaxNumber");
            RenameColumn(table: "dbo.PlanProcuringEntities", name: "ContactPoint_Url", newName: "СontactPoint_Url");
            RenameColumn(table: "dbo.AwardComplaintAuthors", name: "ContactPoint_Name", newName: "СontactPoint_Name");
            RenameColumn(table: "dbo.AwardComplaintAuthors", name: "ContactPoint_Email", newName: "СontactPoint_Email");
            RenameColumn(table: "dbo.AwardComplaintAuthors", name: "ContactPoint_Telephone", newName: "СontactPoint_Telephone");
            RenameColumn(table: "dbo.AwardComplaintAuthors", name: "ContactPoint_FaxNumber", newName: "СontactPoint_FaxNumber");
            RenameColumn(table: "dbo.AwardComplaintAuthors", name: "ContactPoint_Url", newName: "СontactPoint_Url");
            RenameColumn(table: "dbo.TenderComplaintAuthors", name: "ContactPoint_Name", newName: "СontactPoint_Name");
            RenameColumn(table: "dbo.TenderComplaintAuthors", name: "ContactPoint_Email", newName: "СontactPoint_Email");
            RenameColumn(table: "dbo.TenderComplaintAuthors", name: "ContactPoint_Telephone", newName: "СontactPoint_Telephone");
            RenameColumn(table: "dbo.TenderComplaintAuthors", name: "ContactPoint_FaxNumber", newName: "СontactPoint_FaxNumber");
            RenameColumn(table: "dbo.TenderComplaintAuthors", name: "ContactPoint_Url", newName: "СontactPoint_Url");
            RenameColumn(table: "dbo.ContractSuppliers", name: "ContactPoint_Name", newName: "СontactPoint_Name");
            RenameColumn(table: "dbo.ContractSuppliers", name: "ContactPoint_Email", newName: "СontactPoint_Email");
            RenameColumn(table: "dbo.ContractSuppliers", name: "ContactPoint_Telephone", newName: "СontactPoint_Telephone");
            RenameColumn(table: "dbo.ContractSuppliers", name: "ContactPoint_FaxNumber", newName: "СontactPoint_FaxNumber");
            RenameColumn(table: "dbo.ContractSuppliers", name: "ContactPoint_Url", newName: "СontactPoint_Url");
            RenameColumn(table: "dbo.ProcuringEntities", name: "ContactPoint_Name", newName: "СontactPoint_Name");
            RenameColumn(table: "dbo.ProcuringEntities", name: "ContactPoint_Email", newName: "СontactPoint_Email");
            RenameColumn(table: "dbo.ProcuringEntities", name: "ContactPoint_Telephone", newName: "СontactPoint_Telephone");
            RenameColumn(table: "dbo.ProcuringEntities", name: "ContactPoint_FaxNumber", newName: "СontactPoint_FaxNumber");
            RenameColumn(table: "dbo.ProcuringEntities", name: "ContactPoint_Url", newName: "СontactPoint_Url");
            RenameColumn(table: "dbo.QuestionAuthors", name: "ContactPoint_Name", newName: "СontactPoint_Name");
            RenameColumn(table: "dbo.QuestionAuthors", name: "ContactPoint_Email", newName: "СontactPoint_Email");
            RenameColumn(table: "dbo.QuestionAuthors", name: "ContactPoint_Telephone", newName: "СontactPoint_Telephone");
            RenameColumn(table: "dbo.QuestionAuthors", name: "ContactPoint_FaxNumber", newName: "СontactPoint_FaxNumber");
            RenameColumn(table: "dbo.QuestionAuthors", name: "ContactPoint_Url", newName: "СontactPoint_Url");
            RenameColumn(table: "dbo.Tenderers", name: "ContactPoint_Name", newName: "СontactPoint_Name");
            RenameColumn(table: "dbo.Tenderers", name: "ContactPoint_Email", newName: "СontactPoint_Email");
            RenameColumn(table: "dbo.Tenderers", name: "ContactPoint_Telephone", newName: "СontactPoint_Telephone");
            RenameColumn(table: "dbo.Tenderers", name: "ContactPoint_FaxNumber", newName: "СontactPoint_FaxNumber");
            RenameColumn(table: "dbo.Tenderers", name: "ContactPoint_Url", newName: "СontactPoint_Url");
            RenameColumn(table: "dbo.Suppliers", name: "ContactPoint_Name", newName: "СontactPoint_Name");
            RenameColumn(table: "dbo.Suppliers", name: "ContactPoint_Email", newName: "СontactPoint_Email");
            RenameColumn(table: "dbo.Suppliers", name: "ContactPoint_Telephone", newName: "СontactPoint_Telephone");
            RenameColumn(table: "dbo.Suppliers", name: "ContactPoint_FaxNumber", newName: "СontactPoint_FaxNumber");
            RenameColumn(table: "dbo.Suppliers", name: "ContactPoint_Url", newName: "СontactPoint_Url");
            RenameColumn(table: "dbo.UserOrganizations", name: "ContactPoint_Name", newName: "СontactPoint_Name");
            RenameColumn(table: "dbo.UserOrganizations", name: "ContactPoint_Email", newName: "СontactPoint_Email");
            RenameColumn(table: "dbo.UserOrganizations", name: "ContactPoint_Telephone", newName: "СontactPoint_Telephone");
            RenameColumn(table: "dbo.UserOrganizations", name: "ContactPoint_FaxNumber", newName: "СontactPoint_FaxNumber");
            RenameColumn(table: "dbo.UserOrganizations", name: "ContactPoint_Url", newName: "СontactPoint_Url");
            AlterColumn("dbo.Tenders", "Status", c => c.String(maxLength: 32));
        }
    }
}