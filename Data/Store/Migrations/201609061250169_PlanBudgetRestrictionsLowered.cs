namespace Kapitalist.Data.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlanBudgetRestrictionsLowered : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DraftPlans", "Budget_Id", c => c.String(maxLength: 64));
            AlterColumn("dbo.DraftPlans", "Budget_Description", c => c.String());
            AlterColumn("dbo.Plans", "Budget_Id", c => c.String(maxLength: 64));
            AlterColumn("dbo.Plans", "Budget_Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Plans", "Budget_Description", c => c.String(nullable: false));
            AlterColumn("dbo.Plans", "Budget_Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.DraftPlans", "Budget_Description", c => c.String(nullable: false));
            AlterColumn("dbo.DraftPlans", "Budget_Id", c => c.String(nullable: false, maxLength: 32));
        }
    }
}
