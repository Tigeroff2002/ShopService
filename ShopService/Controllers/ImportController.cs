using Microsoft.AspNetCore.Mvc;
using ShopService.Import.Storage;
namespace ShopService.Controllers
{
    public class ImportController : ControllerBase
    {
        public readonly ILogger<ImportController> _logger;
        private readonly ImportContext _context;
        public ImportController(ILogger<ImportController> logger, ImportContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IEnumerable<string> Get() => _context.Histories.Select(x => x.DeviceName).ToList()!;
    }
}