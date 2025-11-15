using System;
using System.Collections.Generic;
using HotelBookingSystemAPI.Domain.Entities;

namespace HotelBookingSystemAPI.Application.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public RoomStatus Status { get; set; }
        public decimal PricePerNight { get; set; }

        public List<int> BookingIds { get; set; } = new();
    }
}
