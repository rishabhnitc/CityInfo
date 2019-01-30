using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {

        [HttpGet("{cityId}/pointsOfInterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var temp = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (temp == null)
            {
                return NotFound();
            }
            return Ok(temp.PointsOfInterest);
        }

        [HttpGet("{cityId}/pointsOfInterest/{id}")]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {
            var temp = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (temp == null)
            {
                return NotFound();
            }

            var pt = temp.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if(pt == null)
            {
                return NotFound();
            }
            return Ok(pt);
        }

    }
}
