using System;
using System.Data.Entity.Migrations;
using Kapitalist.Web.Client.Models.Identity.Entities;

namespace Kapitalist.Web.Client.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Web.Client.Models.Identity.IdentityDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Web.Client.Models.Identity.IdentityDb context)
        {
            context.AspNetRoles.Add(new AspNetRole { Id = Guid.NewGuid().ToString(), Name = "SeniorAdministrator", NameCyrillic = "Старший адміністратор", Description = "Має повний доступ", Discriminator = "ApplicationRole" });
            context.AspNetRoles.Add(new AspNetRole { Id = Guid.NewGuid().ToString(), Name = "Administrator", NameCyrillic = "Адміністратор", Description = "Має повний доступ окрім користувачів з ролі \"Старший адміністратор\"", Discriminator = "ApplicationRole" });
            context.AspNetRoles.Add(new AspNetRole { Id = Guid.NewGuid().ToString(), Name = "AuthorizedUser", NameCyrillic = "Авторизований користувач", Description = "Має повний доступ до функціоналу майданчика окрім адміністративних функцій", Discriminator = "ApplicationRole" });
            context.AspNetRoles.Add(new AspNetRole { Id = Guid.NewGuid().ToString(), Name = "NotAuthorizedUser", NameCyrillic = "Не авторизований користувач", Description = "Має доступ до перегляду статистичних данних, тендерів, тощо", Discriminator = "ApplicationRole" });
            context.SaveChanges();
        }

    }
}