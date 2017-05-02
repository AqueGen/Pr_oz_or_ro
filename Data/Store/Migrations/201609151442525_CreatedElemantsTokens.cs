namespace Kapitalist.Data.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedElemantsTokens : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreatedAwards",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Token = c.String(maxLength: 128),
                        UserOrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Awards", t => t.Id)
                .ForeignKey("dbo.UserOrganizations", t => t.UserOrganizationId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.UserOrganizationId);
            
            CreateTable(
                "dbo.CreatedAwardComplaints",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Token = c.String(maxLength: 128),
                        UserOrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AwardComplaints", t => t.Id)
                .ForeignKey("dbo.UserOrganizations", t => t.UserOrganizationId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.UserOrganizationId);
            
            CreateTable(
                "dbo.CreatedBids",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Token = c.String(maxLength: 128),
                        UserOrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bids", t => t.Id)
                .ForeignKey("dbo.UserOrganizations", t => t.UserOrganizationId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.UserOrganizationId);
            
            CreateTable(
                "dbo.CreatedContracts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Token = c.String(maxLength: 128),
                        UserOrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.Id)
                .ForeignKey("dbo.UserOrganizations", t => t.UserOrganizationId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.UserOrganizationId);
            
            CreateTable(
                "dbo.CreatedTenderComplaints",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Token = c.String(maxLength: 128),
                        UserOrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TenderComplaints", t => t.Id)
                .ForeignKey("dbo.UserOrganizations", t => t.UserOrganizationId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.UserOrganizationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreatedTenderComplaints", "UserOrganizationId", "dbo.UserOrganizations");
            DropForeignKey("dbo.CreatedTenderComplaints", "Id", "dbo.TenderComplaints");
            DropForeignKey("dbo.CreatedContracts", "UserOrganizationId", "dbo.UserOrganizations");
            DropForeignKey("dbo.CreatedContracts", "Id", "dbo.Contracts");
            DropForeignKey("dbo.CreatedBids", "UserOrganizationId", "dbo.UserOrganizations");
            DropForeignKey("dbo.CreatedBids", "Id", "dbo.Bids");
            DropForeignKey("dbo.CreatedAwardComplaints", "UserOrganizationId", "dbo.UserOrganizations");
            DropForeignKey("dbo.CreatedAwardComplaints", "Id", "dbo.AwardComplaints");
            DropForeignKey("dbo.CreatedAwards", "UserOrganizationId", "dbo.UserOrganizations");
            DropForeignKey("dbo.CreatedAwards", "Id", "dbo.Awards");
            DropIndex("dbo.CreatedTenderComplaints", new[] { "UserOrganizationId" });
            DropIndex("dbo.CreatedTenderComplaints", new[] { "Id" });
            DropIndex("dbo.CreatedContracts", new[] { "UserOrganizationId" });
            DropIndex("dbo.CreatedContracts", new[] { "Id" });
            DropIndex("dbo.CreatedBids", new[] { "UserOrganizationId" });
            DropIndex("dbo.CreatedBids", new[] { "Id" });
            DropIndex("dbo.CreatedAwardComplaints", new[] { "UserOrganizationId" });
            DropIndex("dbo.CreatedAwardComplaints", new[] { "Id" });
            DropIndex("dbo.CreatedAwards", new[] { "UserOrganizationId" });
            DropIndex("dbo.CreatedAwards", new[] { "Id" });
            DropTable("dbo.CreatedTenderComplaints");
            DropTable("dbo.CreatedContracts");
            DropTable("dbo.CreatedBids");
            DropTable("dbo.CreatedAwardComplaints");
            DropTable("dbo.CreatedAwards");
        }
    }
}
