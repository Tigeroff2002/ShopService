using ShopService.Models;
using ShopService.Data;
namespace ShopService.Controllers
{
    public class ClientController : ProfileController
    {
        public ClientController(ILogger<ProfileController> logger, RetailStoreProfileContext context)
            : base(logger, context) { }

    }
}
