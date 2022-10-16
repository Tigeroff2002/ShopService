using Microsoft.AspNetCore.Mvc;
using ShopService.Models;
using System.Diagnostics;
using ShopService.Data;
using Microsoft.Data.SqlClient;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ShopService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, RetailStoreDataContext context)
        {
            _logger = logger;
            _context = context;
        }
        private readonly RetailStoreDataContext _context;
        public IList<Product>? Devices { get; set; }
        [HttpGet("product/{devicetype}")]
        public List<Product> GetProductsByDeviceType(int deviceTypeId)
        {
            return _context.Products.Where(x => x.DeviceType!.Id == deviceTypeId)
                                    .ToList();
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                Devices = _context.Products.ToList();
            }
            catch (InvalidOperationException)
            {
                Devices = new List<Product>();
            }
            return View(Devices);
        }

        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id is null)
                return BadRequest("Некорректный ресурс!");
            Product? device = _context!.Products.FirstOrDefault(x => x.Id == Id);
            if (device is null)
                return NotFound("Устройство не найдено!");
            return View(device);
        }
        [HttpPost]
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

        [HttpPost]
        public IActionResult Edit(Product device)
        {
            return View(device); 
        }

        [HttpPost]
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