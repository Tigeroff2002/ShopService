using Microsoft.AspNetCore.Mvc;
using ShopService.Models;
using System.Diagnostics;
using Models;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;

namespace ShopService.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RetailStoreDataContext _context;
        private IList<Product>? Devices { get; set; }

        public HomeController(ILogger<HomeController> logger, RetailStoreDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("devicetype/{deviceTypeId:int}")]
        public IActionResult GetProductsByDeviceType(int deviceTypeId)
        {
            Devices = _context.Products.Where(x => x.DeviceType!.Id == deviceTypeId)
                                       .ToList();

            return View(Devices);
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                Devices = _context.Products.ToList();
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                Devices = new List<Product>();
            }
            catch (InvalidOperationException)
            {
                Devices = new List<Product>();
            }
            return View(Devices);
        }

        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest("Некорректный ресурс!");
            Product? device = _context!.Products.FirstOrDefault(x => x.Id == id);
            if (device is null)
                return NotFound("Устройство не найдено!");
            return View("Details", device);
        }

        [HttpPost("{id:int}")]
        public IActionResult Create([Bind(include: "DeviceTypeId, Name, ProducerId")] Product device)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(device);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(device);
        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(Product device)
        {
            return View(device); 
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int? Id)
        {
            Product? device = _context!.Products.FirstOrDefault(x => x.Id == Id);
            return View(device);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}