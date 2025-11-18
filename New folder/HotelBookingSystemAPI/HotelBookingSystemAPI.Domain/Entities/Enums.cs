using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelBookingSystemAPI.Domain.Entities
{
    public enum RoomStatus { Available, Booked, UnderMaintenance }
    public enum BookingStatus { Pending, Confirmed, Cancelled, Completed }
    public enum PaymentStatus { Pending, Paid, Failed }
    public enum PaymentMethod { Cash, Card, UPI, Online }
    public enum HotelSortBy { Default, Rating, PriceHighToLow, PriceLowToHigh }
}
