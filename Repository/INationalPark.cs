using ParkAPI.Models;
using System.Collections.Generic;

namespace ParkAPI.Repository
{
    public interface INationalPark
    {
        ICollection<Models.NationalPark> GetNationalParks();
        Models.NationalPark GetNationalParks(int nationalParkId);
        bool NationalParkExists(string name);
        bool NationalParkExists(int nationalParkId);
        bool CreateNationalPark(Models.NationalPark nationalPark);
        bool UpdateNationalPark(Models.NationalPark nationalPark);
        bool DeleteNationalPark(Models.NationalPark nationalPark);
        bool Save();

    }
}
