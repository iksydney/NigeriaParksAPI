using Microsoft.EntityFrameworkCore;
using ParkAPI.Models;

namespace ParkAPI.Folder
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<NationalPark> NationalParks { get; set; }
    }
}
