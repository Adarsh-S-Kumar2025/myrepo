using System;
using System.Collections.Generic;
using HotelBookingSystemAPI.Domain.Entities;

namespace HotelBookingSystemAPI.Application.DTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalAmount { get; set; }

        // Payment may be null/absent
        public int? PaymentId { get; set; }
    }
}
