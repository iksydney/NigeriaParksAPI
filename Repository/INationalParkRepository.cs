using ParkAPI.Models;
using System.Collections.Generic;

namespace ParkAPI.Repository
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
