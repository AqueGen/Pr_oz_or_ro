namespace Kapitalist.Data.Store.Models
{
    public class CreatedTender : CreatedElementBase
    {
        public virtual Tender Tender { get; set; }
    }
}