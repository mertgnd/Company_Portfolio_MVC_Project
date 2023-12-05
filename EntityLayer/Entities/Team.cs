using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities
{
    public class Team
    {
        public int TeamID { get; set; }
        public string PersonName { get; set; }
        public string Title { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string? ImageUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? TwitterUrl { get; set; }

    }
}