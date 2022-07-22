using System;

namespace ParkAPI.Dtos
{
    public class NationalParkDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public DateTime Established { get; set; }
        public float Area { get; set; }
    }
}
