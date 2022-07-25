using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkAPI.Models
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Trail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public enum DifficultyLevel { Easy, Moderate, Difficult, Expert}
        public DifficultyLevel Difficult { get; set; }
        [Required]
        public int NationalParkId { get; set; }
        [ForeignKey("NationalParkId")]
        public NationalPark NationalPark { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member