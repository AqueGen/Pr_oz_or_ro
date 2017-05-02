namespace Kapitalist.Data.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationAddressStringLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AwardComplaintAuthors", "Name", c => c.String());
            AlterColumn("dbo.AwardComplaintAuthors", "Address_Locality", c => c.String());
            AlterColumn("dbo.AwardComplaintAuthors", "Address_PostalCode", c => c.String(maxLength: 128));
            AlterColumn("dbo.TenderComplaintAuthors", "Name", c => c.String());
            AlterColumn("dbo.TenderComplaintAuthors", "Address_Locality", c => c.String());
            AlterColumn("dbo.TenderComplaintAuthors", "Address_PostalCode", c => c.String(maxLength: 128));
            AlterColumn("dbo.Items", "DeliveryAddress_Locality", c => c.String());
            AlterColumn("dbo.Items", "DeliveryAddress_PostalCode", c => c.String(maxLength: 128));
            AlterColumn("dbo.ContractSuppliers", "Name", c => c.String());
            AlterColumn("dbo.ContractSuppliers", "Address_Locality", c => c.String());
            AlterColumn("dbo.ContractSuppliers", "Address_PostalCode", c => c.String(maxLength: 128));
            AlterColumn("dbo.ProcuringEntities", "Name", c => c.String());
            AlterColumn("dbo.ProcuringEntities", "Address_Locality", c => c.String());
            AlterColumn("dbo.ProcuringEntities", "Address_PostalCode", c => c.String(maxLength: 128));
            AlterColumn("dbo.QuestionAuthors", "Name", c => c.String());
            AlterColumn("dbo.QuestionAuthors", "Address_Locality", c => c.String());
            AlterColumn("dbo.QuestionAuthors", "Address_PostalCode", c => c.String(maxLength: 128));
            AlterColumn("dbo.Tenderers", "Name", c => c.String());
            AlterColumn("dbo.Tenderers", "Address_Locality", c => c.String());
            AlterColumn("dbo.Tenderers", "Address_PostalCode", c => c.String(maxLength: 128));
            AlterColumn("dbo.Suppliers", "Name", c => c.String());
            AlterColumn("dbo.Suppliers", "Address_Locality", c => c.String());
            AlterColumn("dbo.Suppliers", "Address_PostalCode", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserOrganizations", "Name", c => c.String());
            AlterColumn("dbo.UserOrganizations", "Address_Locality", c => c.String());
            AlterColumn("dbo.UserOrganizations", "Address_PostalCode", c => c.String(maxLength: 128));
            AlterColumn("dbo.DraftItems", "DeliveryAddress_Locality", c => c.String());
            AlterColumn("dbo.DraftItems", "DeliveryAddress_PostalCode", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DraftItems", "DeliveryAddress_PostalCode", c => c.String(maxLength: 32));
            AlterColumn("dbo.DraftItems", "DeliveryAddress_Locality", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserOrganizations", "Address_PostalCode", c => c.String(maxLength: 32));
            AlterColumn("dbo.UserOrganizations", "Address_Locality", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserOrganizations", "Name", c => c.String(maxLength: 256));
            AlterColumn("dbo.Suppliers", "Address_PostalCode", c => c.String(maxLength: 32));
            AlterColumn("dbo.Suppliers", "Address_Locality", c => c.String(maxLength: 128));
            AlterColumn("dbo.Suppliers", "Name", c => c.String(maxLength: 256));
            AlterColumn("dbo.Tenderers", "Address_PostalCode", c => c.String(maxLength: 32));
            AlterColumn("dbo.Tenderers", "Address_Locality", c => c.String(maxLength: 128));
            AlterColumn("dbo.Tenderers", "Name", c => c.String(maxLength: 256));
            AlterColumn("dbo.QuestionAuthors", "Address_PostalCode", c => c.String(maxLength: 32));
            AlterColumn("dbo.QuestionAuthors", "Address_Locality", c => c.String(maxLength: 128));
            AlterColumn("dbo.QuestionAuthors", "Name", c => c.String(maxLength: 256));
            AlterColumn("dbo.ProcuringEntities", "Address_PostalCode", c => c.String(maxLength: 32));
            AlterColumn("dbo.ProcuringEntities", "Address_Locality", c => c.String(maxLength: 128));
            AlterColumn("dbo.ProcuringEntities", "Name", c => c.String(maxLength: 256));
            AlterColumn("dbo.ContractSuppliers", "Address_PostalCode", c => c.String(maxLength: 32));
            AlterColumn("dbo.ContractSuppliers", "Address_Locality", c => c.String(maxLength: 128));
            AlterColumn("dbo.ContractSuppliers", "Name", c => c.String(maxLength: 256));
            AlterColumn("dbo.Items", "DeliveryAddress_PostalCode", c => c.String(maxLength: 32));
            AlterColumn("dbo.Items", "DeliveryAddress_Locality", c => c.String(maxLength: 128));
            AlterColumn("dbo.TenderComplaintAuthors", "Address_PostalCode", c => c.String(maxLength: 32));
            AlterColumn("dbo.TenderComplaintAuthors", "Address_Locality", c => c.String(maxLength: 128));
            AlterColumn("dbo.TenderComplaintAuthors", "Name", c => c.String(maxLength: 256));
            AlterColumn("dbo.AwardComplaintAuthors", "Address_PostalCode", c => c.String(maxLength: 32));
            AlterColumn("dbo.AwardComplaintAuthors", "Address_Locality", c => c.String(maxLength: 128));
            AlterColumn("dbo.AwardComplaintAuthors", "Name", c => c.String(maxLength: 256));
        }
    }
}
