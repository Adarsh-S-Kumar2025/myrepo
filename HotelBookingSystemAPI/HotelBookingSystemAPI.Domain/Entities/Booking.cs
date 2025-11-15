using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalAmount { get; set; }

        public Customer Customer { get; set; } = null!;
        public Room Room { get; set; } = null!;
        public Payment? Payment { get; set; }
    }
}
