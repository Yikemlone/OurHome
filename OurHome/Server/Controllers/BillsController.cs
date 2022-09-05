using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<BillsDto> GetBills()
        {
            var bills = _billsService.GetBills();
            List<BillsDto> retVal = (List<BillsDto>) bills.Result;

            return retVal;
        }

        [HttpGet]
        [Route("/people/{person}")]
        public PersonsBillsDto GetPersonsBills(string person)
        {
            var bill = _billsService.GetPersonsBills(person);
            PersonsBillsDto retVal = bill.Result;

            return retVal;
        }   
        
        [HttpGet]
        [Route("/people")]
        public IEnumerable<PersonsBillsDto> GetPeoplesBills()
        {
            var bills = _billsService.GetPeoplesBills();
            List<PersonsBillsDto> retVal = (List<PersonsBillsDto>)bills.Result;

            return retVal;
        }

        [HttpPost]
        public Task SetBills([FromBody] BillsDto updatedBills)
        {
            _billsService.UpdateBills(updatedBills);
            return Task.CompletedTask;
        }
    }
}
