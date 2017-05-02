namespace Kapitalist.Data.Store.Models
{
    public class CreatedTenderComplaint : CreatedElementBase
    {
        public virtual TenderComplaint TenderComplaint { get; set; }
    }
}