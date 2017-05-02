using Kapitalist.Data.Store.Models;
using Newtonsoft.Json;
using System.Data.Entity.Migrations;
using System.IO;
using System.Reflection;

namespace Kapitalist.Data.Store.Migrations.Seed
{
    /// <summary>
    /// Заповнює в базі схеми класифікацій з вбудованого ресурса.
    /// Файл ClassificationSchemes.json можна завантажити звідси:
    /// http://iatistandard.org/202/codelists/OrganisationRegistrationAgency/
    /// </summary>
    public static class ClassificationSchemes
    {
        public static void Seed(StoreContext context)
        {
            Schemes schemes;
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(typeof(ClassificationSchemes).FullName))
            using (StreamReader reader = new StreamReader(stream))
            {
                JsonSerializer serializer = new JsonSerializer();
                schemes = (Schemes)serializer.Deserialize(reader, typeof(Schemes));
            }

            context.ClassificationSchemes.AddOrUpdate(s => s.Scheme, schemes.ClassificationSchemes);
            context.SaveChanges();
        }

        private class Schemes
        {
            [JsonProperty("data")]
            public ClassificationScheme[] ClassificationSchemes { get; set; }
        }
    }
}