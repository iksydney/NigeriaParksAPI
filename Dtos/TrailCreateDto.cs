using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ParkAPI.Models.Trail;

namespace ParkAPI.Dtos
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrailCreateDto

    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public DifficultyLevel Difficult { get; set; }
        [Required]
        public int NationalParkId { get; set; }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
