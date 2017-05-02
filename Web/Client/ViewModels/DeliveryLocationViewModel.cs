using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels
{
    public class DeliveryLocationViewModel: IEmpty
    {
        public string Elevation { get; set; }

        //[Required]
        public string Latitude { get; set; }

        //[Required]
        public string Longitude { get; set; }

        public DeliveryLocationViewModel(DeliveryLocation dto)
        {
            Elevation = dto.Elevation;
            Latitude = dto.Latitude;
            Longitude = dto.Longitude;
        }

        public DeliveryLocationViewModel()
        {
        }


        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(Latitude)
                   || string.IsNullOrWhiteSpace(Longitude);
        }

        public DeliveryLocation ToDTO()
        {
            if (string.IsNullOrWhiteSpace(Elevation) || string.IsNullOrWhiteSpace(Latitude) ||
                string.IsNullOrWhiteSpace(Longitude))
            {
                return null;
            }

            return new DeliveryLocation
            {
                Elevation = Elevation,
                Latitude = Latitude,
                Longitude = Longitude
            };
        }
    }
}