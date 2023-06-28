using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TWSE_Trade_Web_API.Service.Interface;
using TWSE_Trade_Web_API.ServiceModel;
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
        [HttpGet]
        public async Task<TradeQueryRespViewModel> GetTradesInformationAsync([FromQuery] TradeQueryViewModel viewModel)
        {
            var serviceModel = _mapper.Map<TradeQueryServiceModel>(viewModel);
            var respServiceModel = await _tradeService.ReadTradesInformationAsync(serviceModel);
            var respViewModel = _mapper.Map<TradeQueryRespViewModel>(respServiceModel);
            return respViewModel;
        }

        // GET api/<TradeController>/5
        [HttpGet("{id}")]
        public async Task<TradeRespViewModel> GetTradeInformationByIdAsync(int id)
        {
            var respServiceModel = await _tradeService.ReadTradeInformatoinByIdAsync(id);
            var respViewModel = _mapper.Map<TradeRespViewModel>(respServiceModel);
            return respViewModel;
        }

        // PUT api/<TradeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTradeInformationByIdAsync(int id, [FromBody] TradeUpdateViewModel viewModel)
        {
            var user = "User";
            var serviceModel = _mapper.Map<TradeUpdateServiceModel>(viewModel);
            var rowschange = await _tradeService.UpdateTradeByIdAsync(id,user,serviceModel);
            if (rowschange <= 0)
                return BadRequest("ID Not Found");
            else
                return Ok(rowschange);
        }

        // DELETE api/<TradeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTradeByIdAsync(int id)
        {
            var user = "User";
            var rowschange = await _tradeService.UpdateTradeStatusWithDeletedCodeByIDAsync(id, user);
            if (rowschange <= 0)
                return BadRequest("ID Not Found");
            else
                return Ok(rowschange);
        }
    }
}
