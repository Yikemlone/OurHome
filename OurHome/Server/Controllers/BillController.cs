using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurHome.Models.Models;

namespace OurHome.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class BillController : ControllerBase
    {

        [HttpGet]
        [Route("all")]
        public Task<List<Bill>> GetAllBills() 
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{ID}")]
        public Task<Bill> GetBill() 
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("add")]
        public async Task AddBill([FromBody] Bill bill) 
        { 

        }

        [HttpPost]
        [Route("update")]
        public async Task UpdateBill([FromBody] Bill bill) 
        { 
        
        }

        [HttpPost]
        [Route("delete")]
        public async Task DeleteBill([FromBody] Bill bill)
        {

        }

    }
} 
