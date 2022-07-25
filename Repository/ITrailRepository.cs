using ParkAPI.Models;
using System.Collections.Generic;

namespace ParkAPI.Repository
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrails();
        ICollection<Trail> GetTrailsFromNationalPark(int nationalParkId);
        Trail GetTrails(int trailsId);
        bool TrailExists(string name);
        bool TrailExists(int trailId);
        bool CreateTrail(Trail trail);
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool Save();
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
