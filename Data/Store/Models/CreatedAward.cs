namespace Kapitalist.Data.Store.Models
{
    public class CreatedAward : CreatedElementBase
    {
        public virtual Award Award { get; set; }
    }
}