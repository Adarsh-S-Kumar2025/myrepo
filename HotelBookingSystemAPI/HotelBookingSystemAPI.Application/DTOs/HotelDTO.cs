using System;
using System.Collections.Generic;

namespace HotelBookingSystemAPI.Application.DTOs
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        // Relations represented by Id collections to avoid deep object graphs in DTOs
        public List<int> RoomIds { get; set; } = new();
        public List<int> EmployeeIds { get; set; } = new();
        public List<int> ReviewIds { get; set; } = new();
    }
}
