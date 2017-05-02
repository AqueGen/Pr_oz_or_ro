namespace Kapitalist.Data.Store.Models
{
    public class CreatedContract : CreatedElementBase
    {
        public virtual Contract Contract { get; set; }
    }
}