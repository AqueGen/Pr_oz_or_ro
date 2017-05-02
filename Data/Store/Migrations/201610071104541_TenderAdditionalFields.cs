namespace Kapitalist.Data.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TenderAdditionalFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tenders", "ProcurementMethod", c => c.String(maxLength: 32));
            AddColumn("dbo.Tenders", "ProcurementMethodRationale", c => c.String());
            AddColumn("dbo.Tenders", "ProcurementMethodType", c => c.String(maxLength: 32));
            AddColumn("dbo.Tenders", "Cause", c => c.String(maxLength: 32));
            AddColumn("dbo.Tenders", "CauseDescription", c => c.String());
            AddColumn("dbo.DraftTenders", "ProcurementMethod", c => c.String(maxLength: 32));
            AddColumn("dbo.DraftTenders", "ProcurementMethodRationale", c => c.String());
            AddColumn("dbo.DraftTenders", "ProcurementMethodType", c => c.String(maxLength: 32));
            AddColumn("dbo.DraftTenders", "Cause", c => c.String(maxLength: 32));
            AddColumn("dbo.DraftTenders", "CauseDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DraftTenders", "CauseDescription");
            DropColumn("dbo.DraftTenders", "Cause");
            DropColumn("dbo.DraftTenders", "ProcurementMethodType");
            DropColumn("dbo.DraftTenders", "ProcurementMethodRationale");
            DropColumn("dbo.DraftTenders", "ProcurementMethod");
            DropColumn("dbo.Tenders", "CauseDescription");
            DropColumn("dbo.Tenders", "Cause");
            DropColumn("dbo.Tenders", "ProcurementMethodType");
            DropColumn("dbo.Tenders", "ProcurementMethodRationale");
            DropColumn("dbo.Tenders", "ProcurementMethod");
        }
    }
}
