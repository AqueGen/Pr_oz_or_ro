namespace Kapitalist.Data.Store.Models
{
    public class CreatedBid : CreatedElementBase
    {
        public virtual Bid Bid { get; set; }
    }
}