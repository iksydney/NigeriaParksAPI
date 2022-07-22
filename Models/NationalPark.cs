using System;
using System.ComponentModel.DataAnnotations;

namespace ParkAPI.Models
{
    public class NationalPark
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public DateTime Established { get; set; }
        public float Area { get; set; }
    }
}
