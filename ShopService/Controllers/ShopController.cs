using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace ShopService.Controllers
{
    [ApiController]
    [Route("api/shop")]
    public class ShopController : ControllerBase
    {
        [HttpGet("pushShip/{shipId:int}")]
        public async Task<IActionResult> PushShip(int shipId)
        {
            var task = Task.Run(async () => await Task.Delay(2_000)
                .ConfigureAwait(false));

            task.Wait();

            return RedirectToAction("AuthIndex", "Home", new {id = 2});
        }

        [HttpGet("pullShip/{shipId:int}")]
        public async Task<IActionResult> PullShip(int shipId)
        {
            var task = Task.Run(async () => await Task.Delay(2_000)
                .ConfigureAwait(false));

            task.Wait();

            return RedirectToAction("AuthIndex", "Home", new { id = 2 });
        }
    }
}
