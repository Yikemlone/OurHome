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
        public List<BillDto> GetBills()
        {
            var bills = _billsService.GetBills();
            List<BillDto> retVal = bills.Result.ToList();
            return retVal;
        }

        [HttpGet]
        [Route("people")]
        public async Task<List<PersonsBillsDto>> GetPeoplesBills()
        {
            var personsBills = await _billsService.GetPeoplesBills();
            List<PersonsBillsDto> retVal = personsBills.ToList();
            return retVal;
        }

        [HttpGet]
        [Route("people/{person}")]
        public async Task<PersonsBillsDto> GetPersonsBills(int person)
        {
            var personBills = await _billsService.GetPersonsBills(person);
            PersonsBillsDto retVal = personBills;
            return retVal;
        }

        [HttpPost]
        public async Task UpdateBills([FromBody] BillDto updatedBills)
        {
            await _billsService.UpdateBills(updatedBills);
        }

        [HttpGet]
        [Route("past-bills")]
        public async Task<List<PastBillDto>> GetPastBills()
        {
            var pastBills = await _billsService.GetPastBills();
            List<PastBillDto> retVal = pastBills.ToList();
            return retVal;
        }

        [HttpGet]
        [Route("dates")]
        public async Task<List<BillDueDateDto>> GetBillDueDates()
        {
            var billDueDate = await _billsService.GetBillDueDates();
            List<BillDueDateDto> retVal = billDueDate.ToList();
            return retVal;
        }

        [HttpGet]
        [Route("payed-bills")]
        public async Task<List<PayedBillDto>> GetPayedBills()
        {
            var payedBill = await _billsService.GetPayedBills();
            List<PayedBillDto> retVal = payedBill.ToList();
            return retVal;
        }
    }
}
