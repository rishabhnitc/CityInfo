using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]

    public class CitiesController : Controller
    {
        [HttpGet()]
        public IActionResult GetCities()
        {
            //var temp =  new JsonResult(CitiesDataStore.Current.Cities        
            //    );

            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var result = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
