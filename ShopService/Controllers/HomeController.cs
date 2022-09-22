using Microsoft.AspNetCore.Mvc;
using ShopService.Models;
using System.Diagnostics;
using ShopService.Data;
using Microsoft.Data.SqlClient;
using System.Net;

namespace ShopService.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, RetailStoreDataContext context)
        {
            _logger = logger;
            _context = context;
        }
        private readonly RetailStoreDataContext _context;
        public IList<Device>? Devices { get; set; }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                Devices = _context.Devices.ToList();
            }
            catch (InvalidOperationException)
            {
                Devices = new List<Device>();
            }
            return View(Devices);
        }
        [HttpGet]
        public IActionResult Details(int? Id)
        {
            if (Id is null)
                return BadRequest("Некорректный ресурс!");
            Device? device = _context!.Devices.FirstOrDefault(x => x.Id == Id);
            if (device is null)
                return NotFound("Устройство не найдено!");
            return View(device);
        }
        [HttpPost]
        public IActionResult Create([Bind(include: "DeviceTypeId, Name, ProducerId")] Device device)
        {
            if (ModelState.IsValid)
            {
                _context.Devices.Add(device);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(device);
        }

        [HttpPost]
        public IActionResult Edit(Device device)
        {
            return View(device); 
        }

        [HttpPost]
        public IActionResult Delete(int? Id)
        {
            Device? device = _context!.Devices.FirstOrDefault(x => x.Id == Id);
            return View(device);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}