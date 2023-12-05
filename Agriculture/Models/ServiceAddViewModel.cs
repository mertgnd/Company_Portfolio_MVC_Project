using System.ComponentModel.DataAnnotations;

namespace AgriculturePresentation.Models
{
    public class ServiceAddViewModel
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title can not be null.")]
        public string Title { get; set; }
        [Display(Name = "Image")]
        [Required(ErrorMessage = "ImageURL can not be null.")]
        public string Image { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description can not be null.")]
        public string Description { get; set; }
    }
}
