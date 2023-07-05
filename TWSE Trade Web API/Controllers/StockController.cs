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
    public class StockController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStockService _stockService;
        public StockController(IMapper mapper, IStockService stockService)
        {
            _mapper = mapper;
            _stockService = stockService;
        }
        // GET: api/<StockController>
        [HttpGet]
        public async Task<StockRespViewModel> GetAsync()
        {
            var respServiceModel = await _stockService.ReadStockInformatoinByStockIdAsync("0050");
            var respViewModel = _mapper.Map<StockRespViewModel>(respServiceModel);
            return respViewModel;
        }

        // GET api/<StockController>/5
        [HttpGet("{stockId}")]
        public async Task<StockRespViewModel> GetStockInformationByStockIdAsync(string stockId)
        {
            var respServiceModel = await _stockService.ReadStockInformatoinByStockIdAsync(stockId);
            var respViewModel = _mapper.Map<StockRespViewModel>(respServiceModel);
            return respViewModel;
        }
    }
}
