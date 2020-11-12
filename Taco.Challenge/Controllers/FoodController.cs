using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Taco.Challenge.Infrastructure;
using Taco.Challenge.Restaurant.Exceptions;
using Taco.Challenge.Restaurant.Queries;

namespace Taco.Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly IQueryDispatcherClient _qd;

        public FoodController(IQueryDispatcherClient queryDispatcherClient)
        {
            this._qd = queryDispatcherClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string search)
        {
            if (string.IsNullOrEmpty(search))
                return NotFound("Provide some search info");

            try
            {
                var result = await _qd.QueryAsync(new FoodQuery { SearchText = search });
                return Ok(result.Items);
            }
            catch (RestaurantsNotFoundException)
            {
                return NotFound("There is no restaurants in such location");
            }
            catch 
            {
                throw;
            }
        }
    }
}
