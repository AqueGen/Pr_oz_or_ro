namespace Kapitalist.Data.Models.DTO
{
    public class PlanProcuringEntityIdentifierDTO
    {
        public string Id { get; set; }
        //public int InternalId { get; set; }
        public string Scheme { get; set; }
        public string Uri { get; set; }
        public int OrganizationId { get; set; }
        public bool Main { get; set; }
        public string LegalName { get; set; }
    }
}