using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;
        private readonly ITradeService _tradeService;
        public TradeController(IMapper mapper, ITradeService tradeService)
        {
            _mapper = mapper;
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
        public async Task<TradeRespViewModel> GetTradeInformationById(int id)
        {
            var trsm = await _tradeService.ReadTradeInformatoinByIdAsync(id);
            var trvm = _mapper.Map<TradeRespViewModel>(trsm);
            return trvm;
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
