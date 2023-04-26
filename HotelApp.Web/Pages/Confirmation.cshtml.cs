using HotelLibrary.DataAccess;
using HotelLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelApp.Web.Pages
{
    public class ConfirmationModel : PageModel
    {
        private readonly IHotelDataAccess _da;

        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string LastName { get; set; }

        public BookingFullModel BookingFull { get; set; }

        public ConfirmationModel(IHotelDataAccess da)
        {
            _da = da;
        }

        public void OnGet()
        {
            //Get booking information
            BookingFull = _da.SearchBookings(LastName, StartDate).First();
        }
    }
}
