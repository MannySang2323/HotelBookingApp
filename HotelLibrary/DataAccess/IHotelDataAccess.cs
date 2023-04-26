using HotelLibrary.Model;

namespace HotelLibrary.DataAccess
{
    public interface IHotelDataAccess
    {
        void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId);
        void CheckInGuest(int bookingId);
        List<RoomTypesModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate);
        RoomTypesModel GetRoomTypesById(int id);
        List<BookingFullModel> SearchBookings(string lastName, DateTime? startDate = null);
    }
}