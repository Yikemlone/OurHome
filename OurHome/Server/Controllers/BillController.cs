using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurHome.DataAccess.Context;
using OurHome.DataAccess.Services.BillCoOwnerService;
using OurHome.Model.Models;
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
        private OurHomeDbContext _context;
        private readonly IBillsService _billsService;
        private readonly IBillCoOwnerService _billCoOwnerService;

        public BillController(IBillsService billsService, IBillCoOwnerService billCoOwnerService)
        {
            _billsService = billsService;
            _billCoOwnerService = billCoOwnerService;
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
        public async Task Add([FromBody] CreateBillDTO createBillDTO) 
        {
            // IF > 0 TRUE ADD BOTH OWNER AND CO-OWNER TO DATABASE, DON'T TOUCH ORIGINAL BILL

            // Divide the Bill price by amount of Co-Owners
            // Foreach Co-owner, create a BillPayor Bill
            // Assign each unique user ID
            // Use Bill to create Bill Payors

            Bill bill = await _billsService.AddBillAsync(createBillDTO.Bill);

            if (createBillDTO.BillCoOwners.Count > 0) 
            {
                await _billCoOwnerService.AddAllAsync(createBillDTO.BillCoOwners, bill);
            }



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
