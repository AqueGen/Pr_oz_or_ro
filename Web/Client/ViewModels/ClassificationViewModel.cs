using Kapitalist.Data.Models.DTO;

namespace Kapitalist.Web.Client.ViewModels
{
    public class ClassificationViewModel
    {
        public string Description { get; set; }
        public string Id { get; set; }
        public string Scheme { get; set; } = "CPV";
        public string Uri { get; set; }

        public ClassificationViewModel()
        {
        }

        public ClassificationViewModel(ClassificationDTO classification)
        {
            if (classification != null)
            {
                Id = classification.Id;
                Description = classification.Description;
                Scheme = classification.Scheme;
                Uri = classification.Uri;
            }
        }

        public ClassificationDTO ToDTO()
        {
            return new ClassificationDTO
            {
                Id = Id,
                Description = Description,
                Scheme = Scheme,
                Uri = Uri
            };
        }

        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(Id);
        }
    }
}