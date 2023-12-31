﻿using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                var trsm = await _twseService.UpdateDBFromTwseAPIAsync("20230103");
                return Ok(trsm);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{endDate}")]
        public async Task<IActionResult> UpdateDBWithEndDateAsync(string endDate)
        {
            try
            {
                var respServiceModel = await _twseService.UpdateDBFromTwseAPIAsync(endDate);
                return Ok(respServiceModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }


}
