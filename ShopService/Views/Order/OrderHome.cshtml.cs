using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Helpers;

namespace ShopService.Views.Order
{
    public class OrderHomeModel : PageModel
    {
        [BindProperty]
        public string? FirstName { get; set; }

        [BindProperty]
        public string? LastName { get; set; }

        [BindProperty]
        public string? ContactNumber { get; set; }

        public OrderHomeModel(string firstName, string lastName, string contactNumber)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            ContactNumber = contactNumber ?? throw new ArgumentNullException(nameof(contactNumber));
        }

        public void OnGet(int orderId)
        {
            ViewData["id"] = orderId;
        }

        public void OnPost()
        {
            var firstName = Request.Form["FirstName"];
            var lastName = Request.Form["LastName"];
            var contactNumber = Request.Form["ContactNumber"];

            ViewData["confirmation"] = $"{FirstName} {LastName}, information for confirmation will be sent to {ContactNumber}";
        }
    }
}
