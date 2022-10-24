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
            List<BillsDto> retVal = bills.Result.ToList();

            return retVal;
        }

        [HttpGet]
        [Route("people")]
        public IEnumerable<PersonsBillsDto> GetPeoplesBills()
        {
            var personsBills = _billsService.GetPeoplesBills();
            List<PersonsBillsDto> retVal = personsBills.Result.ToList();

            return retVal;
        }

        [HttpGet]
        [Route("people/{person}")]
        public PersonsBillsDto GetPersonsBills(int person)
        {
            var bill = _billsService.GetPersonsBills(person);
            PersonsBillsDto retVal = bill.Result;

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
