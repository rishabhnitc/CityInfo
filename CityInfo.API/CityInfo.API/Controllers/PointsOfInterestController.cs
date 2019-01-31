using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{cityId}/pointsOfInterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {
            var temp = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (temp == null)
            {
                return NotFound();
            }

            var pt = temp.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if (pt == null)
            {
                return NotFound();
            }


            return Ok(pt);
        }

        [HttpPost("{cityId}/pointsOfInterest")]
        public IActionResult PointOfInterestCreation(int cityId, [FromBody] PointOfInterestCreationDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }



            if (dto.Name == dto.Description)
            {
                ModelState.AddModelError("Description", "Name and description cannot be same.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maxPointOfInterest = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);
            var finalPointofInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterest,
                Name = dto.Name,
                Description = dto.Description
            };
            city.PointsOfInterest.Add(finalPointofInterest);

            return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, id = finalPointofInterest.Id }, finalPointofInterest);
        }

        [HttpPut("{cityId}/pointsOfInterest/{id}")]
        public IActionResult PointOfInterestUpdate(int cityId, int id, [FromBody] PointOfInterestUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            if (dto.Name == dto.Description)
            {
                ModelState.AddModelError("Description", "Name and description cannot be same.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var poi = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            if (poi == null)
            {
                return NotFound();
            }

            poi.Name = dto.Name;
            poi.Description = dto.Description;

            return NoContent();
        }

        [HttpPut("{cityId}/pointsOfInterest/{id}")]
        public IActionResult PointOfInterestPatch(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestUpdateDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }            

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var poi = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            if (poi == null)
            {
                return NotFound();
            }

            var poiForPatch = new PointOfInterestUpdateDto()
            {
                Name = poi.Name,
                Description = poi.Description

            };

            patchDocument.ApplyTo(poiForPatch, ModelState);

            // this one checks patch document ONLY
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (poiForPatch.Name == poiForPatch.Description)
            {
                ModelState.AddModelError("Description", "Name and description cannot be same.");
            }

            // this one patched POI document 
            TryValidateModel(poiForPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            poi.Name = poiForPatch.Name;
            poi.Description = poiForPatch.Description;

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsOfInterest/{id}")]
        public IActionResult PointOfInterestDelete(int cityId, int id)
        {
          
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var poi = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            if (poi == null)
            {
                return NotFound();
            }

            var poiForPatch = new PointOfInterestUpdateDto()
            {
                Name = poi.Name,
                Description = poi.Description

            };

            city.PointsOfInterest.Remove(poi);

            return NoContent();
        }

    }
}
