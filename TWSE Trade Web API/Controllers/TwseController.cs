using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWSE_Trade_Web_API.Service.Interface;

namespace TWSE_Trade_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwseController : ControllerBase
    {
        private readonly ITwseService _twseService;
        public TwseController(ITwseService twseService)
        {
            _twseService = twseService;
        }
        [HttpGet]
        public async Task<IActionResult> UpdateDB()
        {
             _twseService.UpdateDatabaseAsync("20230103");
            return NoContent();
        }
        [HttpGet("{endDate}")]
        public IActionResult UpdateDB(string endDate)
        {
            _twseService.UpdateDatabaseAsync(endDate);
            return NoContent();
        }
    }


}
