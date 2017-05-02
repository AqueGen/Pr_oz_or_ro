namespace Kapitalist.Web.Client.Interfaces
{
    public interface IAddressViewModel
    {
        string Country { get; set; }
        string Locality { get; set; }
        string PostalCode { get; set; }
        string Region { get; set; }
        string Street { get; set; }
    }
}