using HotelLibrary.DataAccess;
using HotelLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Web.Pages
{
    public class BookRoomModel : PageModel
    {
        private readonly IHotelDataAccess _da;

        [BindProperty(SupportsGet = true)]
        public int RoomTypeId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "First name must be given to book room")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last name must be given to book room")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public RoomTypesModel RoomType { get; set; }


        public BookRoomModel(IHotelDataAccess da)
        {
            _da = da;
        }

        public void OnGet()
        {
            //Populate room type info
            RoomType = _da.GetRoomTypesById(RoomTypeId);
        }

        public IActionResult OnPost()
        {

            //Book guest
            _da.BookGuest(FirstName, LastName, StartDate, EndDate, RoomTypeId);

            //Redirect to Confirmation Page
            return RedirectToPage("/Confirmation",
                                  new
                                  {
                                      LastName = this.LastName,
                                      StartDate = this.StartDate.ToString("yyyy-MM-dd")
                                  });

        }
    }
}
