using ShopService.Models;
using ShopService.Data;
namespace ShopService.Controllers
{
    public class ClientController : UserController
    {
        public ClientController(ILogger<UserController> logger, RetailStoreDataContext context)
            : base(logger, context) { }

    }
}
