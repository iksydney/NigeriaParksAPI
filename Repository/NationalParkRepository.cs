using ParkAPI.Folder;
using ParkAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ParkAPI.Repository
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _db;
        public NationalParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);
            return Save();
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(x => x.Name).ToList();
        }

        public NationalPark GetNationalParks(int nationalParkId)
        {
            return _db.NationalParks.FirstOrDefault(a => a.Id == nationalParkId);
        }

        public bool NationalParkExists(string name)
        {
            bool value = _db.NationalParks.Any(a => a.Name.ToLower().Trim() == name.ToLower());
            return value;
        }

        public bool NationalParkExists(int nationalParkId)
        {
            return _db.NationalParks.Any(a => a.Id == nationalParkId);
        }

        public bool Save()
        {
#pragma warning disable IDE0075 // Simplify conditional expression
            return _db.SaveChanges() >= 0 ? true: false;
#pragma warning restore IDE0075 // Simplify conditional expression
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
