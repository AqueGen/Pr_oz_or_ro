namespace Kapitalist.Data.Store.Migrations
{
    using Seed;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Kapitalist.Data.Store.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Kapitalist.Data.Store.StoreContext context)
        {
            ClassificationSchemes.Seed(context);
        }
    }
}