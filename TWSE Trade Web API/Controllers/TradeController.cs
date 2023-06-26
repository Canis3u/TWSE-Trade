using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWSE_Trade_Web_API.Service.Interface;
using TWSE_Trade_Web_API.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TWSE_Trade_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;
        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }
        // GET: api/<TradeController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<TradeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTradeInformationById(int id)
        {
            var r = await _tradeService.SelectTradeByIdAsync(id);
            return Ok();
        }

        // PUT api/<TradeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TradeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
