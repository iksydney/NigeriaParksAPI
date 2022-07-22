using ParkAPI.Folder;
using ParkAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ParkAPI.Repository
{
    public class NationalPark : INationalPark
    {
        private readonly ApplicationDbContext _db;
        public NationalPark(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateNationalPark(Models.NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(Models.NationalPark nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);
            return Save();
        }

        public ICollection<Models.NationalPark> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(x => x.Name).ToList();
        }

        public Models.NationalPark GetNationalParks(int nationalParkId)
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
            return _db.SaveChanges() >= 0 ? true: false;
        }

        public bool UpdateNationalPark(Models.NationalPark nationalPark)
        {
            _db.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
