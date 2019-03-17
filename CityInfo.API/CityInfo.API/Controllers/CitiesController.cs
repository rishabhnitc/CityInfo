using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{

    // add seed data
    // add hosted service
    [Route("api/cities")]

    public class CitiesController : Controller
    {

        private ICityInfoRepository _cityInfoRepository;
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }
        [HttpGet()]
        public IActionResult GetCities()
        {
            //var temp =  new JsonResult(CitiesDataStore.Current.Cities        
            //    );

            var cities = _cityInfoRepository.GetCities();
            var result = AutoMapper.Mapper.Map<IEnumerable<Models.CityDtoWIthoutPOI>>(cities);
         //  var result = cities.Select(c => new CityDtoWIthoutPOI() { Id = c.Id, Description = c.Description, Name = c.Name });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePoi =  false)
        {
            //  var result = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            var city = _cityInfoRepository.GetCity(id, includePoi);
            if (city == null)
            {
                return NotFound();
            }

           
            if (includePoi)
            {
                var result = AutoMapper.Mapper.Map<CityDto>(city);
                //var result = new CityDto() { Id = city.Id, Description = city.Description, Name = city.Name };
                //foreach (var item in city.PointsOfInterest)
                //{
                //    result.PointsOfInterest.Add(new PointOfInterestDto()
                //    {
                //        Id = item.Id,
                //        Name = item.Name,
                //        Description = item.Description
                //    });
                //}
                return Ok(result);
            }
            else
            {
                var result = new CityDtoWIthoutPOI() { Id = city.Id, Description = city.Description, Name = city.Name };
                return Ok(result);
            }           
        }
    }
}
