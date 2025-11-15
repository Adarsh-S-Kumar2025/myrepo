using System;
using System.Collections.Generic;

namespace HotelBookingSystemAPI.Application.DTOs
{
    public class RoomTypeDTO
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }

        public List<int> RoomIds { get; set; } = new();
    }
}
