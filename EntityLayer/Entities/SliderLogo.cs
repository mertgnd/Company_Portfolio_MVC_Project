using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities
{
    public class SliderLogo
    {
        public int Id { get; set; }

        public string SliderTitle { get; set; }
        public string SliderDescription { get; set; }

        public string PopUpTitle { get; set; }
        public string PopUpSubTitle { get; set; }
        public string PopUpDescription { get; set; }

        [NotMapped]
        public IFormFile ImageFileLogo { get; set; }
        public string? ImageUrlLogo { get; set; }

        [NotMapped]
        public IFormFile ImageFileSlider { get; set; }
        public string? ImageUrlSlider { get; set; }

        [NotMapped]
        public IFormFile ImageFilePopUp { get; set; }
        public string? ImageUrlPopUp { get; set; }
    }
}
