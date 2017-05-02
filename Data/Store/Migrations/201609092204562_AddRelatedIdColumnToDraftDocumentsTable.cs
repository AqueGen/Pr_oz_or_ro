namespace Kapitalist.Data.Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelatedIdColumnToDraftDocumentsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DraftTenderDocuments", "RelatedId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DraftTenderDocuments", "RelatedId");
        }
    }
}
