using System;

namespace HotelBookingSystemAPI.Application.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime ReviewDate { get; set; }
    }
}
