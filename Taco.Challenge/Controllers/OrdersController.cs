using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Taco.Challenge.Controllers.Requests;

namespace Taco.Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {

        public OrdersController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]OrderRequest request)
        {
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateOrder()
        {
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> CancelOrder()
        {
            return Ok();
        }
    }
}