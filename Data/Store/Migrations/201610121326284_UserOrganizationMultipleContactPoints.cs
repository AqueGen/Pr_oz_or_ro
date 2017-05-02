namespace Kapitalist.Data.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserOrganizationMultipleContactPoints : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserOrganizationContactPoints",
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
                .ForeignKey("dbo.UserOrganizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            DropColumn("dbo.UserOrganizations", "ContactPoint_Name");
            DropColumn("dbo.UserOrganizations", "ContactPoint_NameEn");
            DropColumn("dbo.UserOrganizations", "ContactPoint_Email");
            DropColumn("dbo.UserOrganizations", "ContactPoint_Telephone");
            DropColumn("dbo.UserOrganizations", "ContactPoint_FaxNumber");
            DropColumn("dbo.UserOrganizations", "ContactPoint_Url");
            DropColumn("dbo.UserOrganizations", "ContactPoint_AvailableLanguage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserOrganizations", "ContactPoint_AvailableLanguage", c => c.String(maxLength: 32));
            AddColumn("dbo.UserOrganizations", "ContactPoint_Url", c => c.String());
            AddColumn("dbo.UserOrganizations", "ContactPoint_FaxNumber", c => c.String(maxLength: 256));
            AddColumn("dbo.UserOrganizations", "ContactPoint_Telephone", c => c.String(maxLength: 256));
            AddColumn("dbo.UserOrganizations", "ContactPoint_Email", c => c.String(maxLength: 256));
            AddColumn("dbo.UserOrganizations", "ContactPoint_NameEn", c => c.String());
            AddColumn("dbo.UserOrganizations", "ContactPoint_Name", c => c.String(nullable: false, maxLength: 256));
            DropForeignKey("dbo.UserOrganizationContactPoints", "OrganizationId", "dbo.UserOrganizations");
            DropIndex("dbo.UserOrganizationContactPoints", new[] { "OrganizationId" });
            DropTable("dbo.UserOrganizationContactPoints");
        }
    }
}
