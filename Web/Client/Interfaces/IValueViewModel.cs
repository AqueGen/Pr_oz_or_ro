namespace Kapitalist.Web.Client.Interfaces
{
    public interface IValueViewModel
    {
        decimal Amount { get; set; }
        string Currency { get; set; }
        bool VATIncluded { get; set; }
    }
}