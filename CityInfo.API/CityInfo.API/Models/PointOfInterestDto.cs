using System.ComponentModel.DataAnnotations;

namespace CityInfo.API
{
    internal class PointOfInterestDto
    {
        public int Id { get; internal set; }
        public string Description { get; internal set; }
        public string Name { get; internal set; }
    }

    public class PointOfInterestCreationDto
    {
        [Required]
        [MaxLength(200)]
        public string Description { get;  set; }
        [Required( ErrorMessage = "Name should be provided.")]
        [MaxLength(50)]
        public string Name { get;  set; }
    }

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