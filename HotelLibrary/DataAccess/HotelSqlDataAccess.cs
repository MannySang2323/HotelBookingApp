using HotelLibrary.Databases;
using HotelLibrary.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLibrary.DataAccess
{
    public class HotelSqlDataAccess : IHotelDataAccess
    {
        private readonly IDataBaseAccess _db;
        private const string ConnStringName = "Default";

        /// <summary>
        /// Set which database to use SqlServer / SqlLite (DI)
        /// </summary>
        /// <param name="db">Which database to use</param>
        public HotelSqlDataAccess(IDataBaseAccess db)
        {
            _db = db;
        }

        /// <summary>
        /// //Get which rooms are not booked by room type
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Room Types of available rooms</returns>
        public List<RoomTypesModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            var output = _db.LoadData<RoomTypesModel, dynamic>("dbo.spRoomTypes_GetAvailableTypes",
                                                        new { startDate = startDate, endDate = endDate },
                                                        ConnStringName,
                                                        true);
            return output;

        }

        /// <summary>
        /// Books guest with dates and room type info (not specific room id)
        /// </summary>
        public void BookGuest(string firstName,
                                    string lastName,
                                    DateTime startDate,
                                    DateTime endDate,
                                    int roomTypeId)
        {
            //Guest - Register guest (assume last name is unique)
            GuestsModel guest = _db.LoadData<GuestsModel, dynamic>("dbo.spGuests_Insert",
                                                                new { firstName = firstName, lastName = lastName },
                                                                ConnStringName,
                                                                true).First();

            //Rooms - Get list of available rooms
            List<RoomsModel> availableRooms = _db.LoadData<RoomsModel, dynamic>("dbo.spRooms_GetAvailableRooms",
                                                                                new { startDate = startDate, endDate = endDate, roomTypeId = roomTypeId },
                                                                                ConnStringName,
                                                                                true);

            //Calculate pricing for total stay
            RoomTypesModel roomType = _db.LoadData<RoomTypesModel, dynamic>("SELECT * FROM dbo.RoomTypes WHERE Id = @id;",
                                                                            new { id = roomTypeId },
                                                                            ConnStringName,
                                                                            false).First();




            TimeSpan durationStay = endDate - startDate;
            decimal totalCost = durationStay.Days * roomType.Price;

            //Create booking
            _db.SaveData<dynamic>("dbo.spBookings_Insert",
                                    new
                                    {
                                        roomId = availableRooms.First().Id,
                                        guestId = guest.Id,
                                        startDate = startDate,
                                        endDate = endDate,
                                        totalCost = totalCost
                                    },
                                    ConnStringName,
                                    true);

        }

        

        /// <summary>
        /// Gets booking
        /// Pass in startDate for future search capabilities
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public List<BookingFullModel> SearchBookings(string lastName, DateTime? startDate = null)
        {   
            DateTime startDateSearch = startDate ?? DateTime.Now.Date;

            var output = _db.LoadData<BookingFullModel, dynamic>("dbo.spBookings_Search",
                                                    new { lastname = lastName, startDate = startDateSearch },
                                                    ConnStringName,
                                                    true);
            return output;
        }

        /// <summary>
        /// Check in guest
        /// </summary>
        /// <param name="bookingId"></param>
        public void CheckInGuest(int bookingId)
        {
            _db.SaveData<dynamic>("dbo.spBookings_CheckInGuest", new { id = bookingId }, ConnStringName, true);
        }

        /// <summary>
        /// Get Room Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoomTypesModel GetRoomTypesById(int id)
        {
            return _db.LoadData<RoomTypesModel, dynamic>("dbo.spRoomTypes_GetById",
                                                         new { id = id },
                                                         ConnStringName,
                                                         true).First();
        }
    }
}
