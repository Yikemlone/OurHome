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
        public async Task Add([FromBody] Bill bill) 
        { 

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
