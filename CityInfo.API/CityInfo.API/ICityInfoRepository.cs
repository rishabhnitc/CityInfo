using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePointOfInterest);
        IEnumerable<PointOfInterest> GetPointOfInterests(int cityId);

        PointOfInterest GetPointOfInterest(int cityId, int pointOfInterestId);
    }

    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            this._context = context;
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityId, bool includePointOfInterest)
        {
            if(includePointOfInterest)
            {
                return _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefault(); 

            }

            return _context.Cities.Where(c => c.Id == cityId).FirstOrDefault();
        }

        public PointOfInterest GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            return _context.PointOfInterests
                 .Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefault();

        }

        public IEnumerable<PointOfInterest> GetPointOfInterests(int cityId)
        {
            return _context.PointOfInterests
                  .Where(p => p.CityId == cityId).ToList();
        }
    }
}
