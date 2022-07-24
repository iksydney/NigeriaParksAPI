using ParkAPI.Models;
using System.Collections.Generic;

namespace ParkAPI.Repository
{
    public interface INationalParkRepository
    {
        ICollection<NationalPark> GetNationalParks();
        NationalPark GetNationalParks(int nationalParkId);
        bool NationalParkExists(string name);
        bool NationalParkExists(int nationalParkId);
        bool CreateNationalPark(NationalPark nationalPark);
        bool UpdateNationalPark(NationalPark nationalPark);
        bool DeleteNationalPark(NationalPark nationalPark);
        bool Save();

    }
}
