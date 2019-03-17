using System.ComponentModel.DataAnnotations;

namespace CityInfo.API
{
    public class PointOfInterestUpdateDto
    {
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Name should be provided.")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}