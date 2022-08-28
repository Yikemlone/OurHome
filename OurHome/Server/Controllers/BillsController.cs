using Microsoft.AspNetCore.Mvc;
using OurHome.Server.Services.Bills;
using OurHome.Shared;

namespace OurHome.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillsController : ControllerBase
    {
        private IBillsService _billsService;

        public BillsController(IBillsService billsService)
        {
            _billsService = billsService;
        }

        [HttpGet]
        public BillsDto GetBills()
        {
            return new BillsDto();
        }
    }
}
