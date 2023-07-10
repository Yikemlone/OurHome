using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurHome.Models.Models;
using OurHome.Server.Services.Bills;
using OurHome.Shared.DTO;

namespace OurHome.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class BillController : ControllerBase
    {
        private IBillsService _billsService;

        public BillController(IBillsService billsService)
        {
            _billsService = billsService;
        }


        [HttpGet]
        [Route("all")]
        public Task<List<Bill>> GetAll() 
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{ID}")]
        public Task<Bill> Get(int ID) 
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("add")]
        public async Task Add([FromBody] CreateBillDTO billDTO) 
        {
            // Create the Bill
            // Return the Bill
            // Check if Co-Oweners is > 0
            // IF TRUE ADD BOTH OWNER AND CO-OWNER TO DATABASE, DON'T TOUCH ORIGINAL BILL
            // Use Bill to create Co-Owners

            // Pass in all Bill Co-Owners
            // Divide the Bill price by amount of Co-Owners
            // Foreach Co-owner, create a BillPayor Bill
            // Assign each unique user ID
            // Use Bill to create Bill Payors

            var bill = await _billsService.AddAsync(billDTO.Bill);
        }

        [HttpPost]
        [Route("update")]
        public async Task Update([FromBody] Bill bill) 
        { 
        
        }

        [HttpPost]
        [Route("delete")]
        public async Task Delete([FromBody] Bill bill)
        {

        }

    }
} 
