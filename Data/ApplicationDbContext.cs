using Microsoft.EntityFrameworkCore;
using ParkAPI.Models;

namespace ParkAPI.Folder
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<Trail> Trails{get; set;}
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
