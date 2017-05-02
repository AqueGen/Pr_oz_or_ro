namespace Kapitalist.Data.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProcuringEntityMultipleContactPoints : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProcuringEntityContactPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganizationId = c.Int(nullable: false),
                        SortingOrder = c.Int(nullable: false),
                        Name = c.String(maxLength: 256),
                        NameEn = c.String(),
                        Email = c.String(maxLength: 256),
                        Telephone = c.String(maxLength: 256),
                        FaxNumber = c.String(maxLength: 256),
                        Url = c.String(),
                        AvailableLanguage = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProcuringEntities", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            AlterColumn("dbo.AwardComplaintAuthors", "ContactPoint_AvailableLanguage", c => c.String(maxLength: 32));
            AlterColumn("dbo.TenderComplaintAuthors", "ContactPoint_AvailableLanguage", c => c.String(maxLength: 32));
            AlterColumn("dbo.ContractSuppliers", "ContactPoint_AvailableLanguage", c => c.String(maxLength: 32));
            AlterColumn("dbo.QuestionAuthors", "ContactPoint_AvailableLanguage", c => c.String(maxLength: 32));
            AlterColumn("dbo.Tenderers", "ContactPoint_AvailableLanguage", c => c.String(maxLength: 32));
            AlterColumn("dbo.Suppliers", "ContactPoint_AvailableLanguage", c => c.String(maxLength: 32));
            AlterColumn("dbo.UserOrganizations", "ContactPoint_AvailableLanguage", c => c.String(maxLength: 32));
            AlterColumn("dbo.PlanProcuringEntities", "ContactPointOptional_AvailableLanguage", c => c.String(maxLength: 32));
            DropColumn("dbo.ProcuringEntities", "ContactPoint_Name");
            DropColumn("dbo.ProcuringEntities", "ContactPoint_NameEn");
            DropColumn("dbo.ProcuringEntities", "ContactPoint_Email");
            DropColumn("dbo.ProcuringEntities", "ContactPoint_Telephone");
            DropColumn("dbo.ProcuringEntities", "ContactPoint_FaxNumber");
            DropColumn("dbo.ProcuringEntities", "ContactPoint_Url");
            DropColumn("dbo.ProcuringEntities", "ContactPoint_AvailableLanguage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProcuringEntities", "ContactPoint_AvailableLanguage", c => c.String());
            AddColumn("dbo.ProcuringEntities", "ContactPoint_Url", c => c.String());
            AddColumn("dbo.ProcuringEntities", "ContactPoint_FaxNumber", c => c.String(maxLength: 256));
            AddColumn("dbo.ProcuringEntities", "ContactPoint_Telephone", c => c.String(maxLength: 256));
            AddColumn("dbo.ProcuringEntities", "ContactPoint_Email", c => c.String(maxLength: 256));
            AddColumn("dbo.ProcuringEntities", "ContactPoint_NameEn", c => c.String());
            AddColumn("dbo.ProcuringEntities", "ContactPoint_Name", c => c.String(nullable: false, maxLength: 256));
            DropForeignKey("dbo.ProcuringEntityContactPoints", "OrganizationId", "dbo.ProcuringEntities");
            DropIndex("dbo.ProcuringEntityContactPoints", new[] { "OrganizationId" });
            AlterColumn("dbo.PlanProcuringEntities", "ContactPointOptional_AvailableLanguage", c => c.String());
            AlterColumn("dbo.UserOrganizations", "ContactPoint_AvailableLanguage", c => c.String());
            AlterColumn("dbo.Suppliers", "ContactPoint_AvailableLanguage", c => c.String());
            AlterColumn("dbo.Tenderers", "ContactPoint_AvailableLanguage", c => c.String());
            AlterColumn("dbo.QuestionAuthors", "ContactPoint_AvailableLanguage", c => c.String());
            AlterColumn("dbo.ContractSuppliers", "ContactPoint_AvailableLanguage", c => c.String());
            AlterColumn("dbo.TenderComplaintAuthors", "ContactPoint_AvailableLanguage", c => c.String());
            AlterColumn("dbo.AwardComplaintAuthors", "ContactPoint_AvailableLanguage", c => c.String());
            DropTable("dbo.ProcuringEntityContactPoints");
        }
    }
}
