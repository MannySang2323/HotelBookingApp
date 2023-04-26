using HotelLibrary.DataAccess;
using HotelLibrary.Databases;
using HotelLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Web.Pages;

public class RoomSearchModel : PageModel
{
    private readonly IHotelDataAccess _da;

    [BindProperty(SupportsGet = true)]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Please provide a start date")]
    [DisplayName("Start Date")]
    public DateTime StartDate { get; set; } = DateTime.Now;

    [BindProperty(SupportsGet = true)]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Please provide an end date")]
    [DisplayName("End Date")]
    public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

    [BindProperty(SupportsGet = true)]
    public List<RoomTypesModel> AvailableRoomTypes { get; set; }

    [BindProperty(SupportsGet = true)]
    public bool IsSearchEnabled { get; set; } = false;


    //Ask DI container for concrete class
    public RoomSearchModel(IHotelDataAccess da)
    {
        _da = da;
        List<RoomTypesModel> AvailableRoomTypes = new List<RoomTypesModel>();
    }

    public void OnGet()
    {
        //Check if we should do our search - coming back from submit
        if (IsSearchEnabled)
        {
            AvailableRoomTypes = _da.GetAvailableRoomTypes(StartDate, EndDate);
        }
    }


    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            if (StartDate <= EndDate)
            {
                //Redirect to the same page but put values into model properties
                return RedirectToPage(new
                {
                    IsSearchEnabled = true,
                    StartDate = this.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = this.EndDate.ToString("yyyy-MM-dd")
                });
            }            
        }

        //Return to same page and display errors
        return Page();

    }


}
