using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        private readonly CityInfoContext _ctx;

        public DummyController(CityInfoContext ctx)
        {
            this._ctx = ctx;
        }

        [HttpGet]
        [Route("testdatabase")]
        public IActionResult GetValue()
        {
            return Ok();
        }

    }
}