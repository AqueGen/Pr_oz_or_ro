namespace Kapitalist.Data.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnquiryPeriodExtended : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tenders", "EnquiryPeriod_InvalidationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Tenders", "EnquiryPeriod_ClarificationsUntil", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.DraftTenders", "EnquiryPeriod_InvalidationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.DraftTenders", "EnquiryPeriod_ClarificationsUntil", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DraftTenders", "EnquiryPeriod_ClarificationsUntil");
            DropColumn("dbo.DraftTenders", "EnquiryPeriod_InvalidationDate");
            DropColumn("dbo.Tenders", "EnquiryPeriod_ClarificationsUntil");
            DropColumn("dbo.Tenders", "EnquiryPeriod_InvalidationDate");
        }
    }
}
