using Microsoft.EntityFrameworkCore;
using ParkAPI.Folder;
using ParkAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ParkAPI.Repository
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _db;
        public TrailRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateTrail(Trail trail)
        {
            _db.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _db.Trails.Remove(trail);
            return Save();
        }

        public ICollection<Trail> GetTrails()
        {
            return _db.Trails.OrderBy(s => s.Name).ToList();
        }

        public Trail GetTrails(int trailsId)
        {
            return _db.Trails.Include(c => c.NationalPark).FirstOrDefault(s => s.Id == trailsId);
        }

        public ICollection<Trail> GetTrailsFromNationalPark(int nationalParkId)
        {
            return _db.Trails.Include(c => c.NationalPark).Where(c => c.NationalParkId == nationalParkId).ToList();
        }

        public bool Save()
        {
            return (_db.SaveChanges() >= 0 ? true : false);
        }

        public bool TrailExists(string name)
        {
            bool value = _db.Trails.Any(a => a.Name.ToLower().Trim() == name.ToLower());
            return value;
        }

        public bool TrailExists(int trailId)
        {
            return _db.Trails.Any(s => s.Id == trailId);
        }

        public bool UpdateTrail(Trail trail)
        {
            _db.Trails.Update(trail);
            return Save();
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
