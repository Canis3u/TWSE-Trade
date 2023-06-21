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
            // For Debug
            var resp = await _twseService.UpdateDatabaseAsync("20230103");
            return Ok(resp);
        }
        [HttpGet("{endDate}")]
        public async Task<IActionResult> UpdateDBWithEndDateAsync(string endDate)
        {
            var resp = await _twseService.UpdateDatabaseAsync(endDate);
            return Ok(resp);
        }
    }


}
