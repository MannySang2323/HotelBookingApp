using HotelLibrary.Databases;
using HotelLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.DataAccess
{
    public class HotelSQL_LiteDataAccess : IHotelDataAccess
    {
        private readonly IDataBaseAccess _db;

        public HotelSQL_LiteDataAccess(IDataBaseAccess db)
        {
            _db = db;
        }

        public void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId)
        {
            throw new NotImplementedException();
        }

        public void CheckInGuest(int bookingId)
        {
            throw new NotImplementedException();
        }

        public List<RoomTypesModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public RoomTypesModel GetRoomTypesById(int id)
        {
            throw new NotImplementedException();
        }

        public List<BookingFullModel> SearchBookings(string lastName)
        {
            throw new NotImplementedException();
        }

        public List<BookingFullModel> SearchBookings(string lastName, DateTime? startDate)
        {
            throw new NotImplementedException();
        }
    }
}
