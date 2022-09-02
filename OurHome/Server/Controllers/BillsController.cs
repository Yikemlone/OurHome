using Microsoft.AspNetCore.Mvc;
using OurHome.Server.Services.Bills;
using OurHome.Shared.DTO;

namespace OurHome.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
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

        [HttpGet]
        [Route("/people/{person}")]
        public BillsDto GetPersonsBills(string person)
        {
            return new BillsDto();
        }   
        
        [HttpGet]
        [Route("/people")]
        public IEnumerable<BillsDto> GetPeoplesBills()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task SetBills([FromBody] BillsDto updatedBills)
        {
            return Task.CompletedTask;
        }
    }
}
