using System;
using System.Collections.Generic;

namespace HotelBookingSystemAPI.Application.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string IdProofNumber { get; set; } = null!;

        public List<int> BookingIds { get; set; } = new();
    }
}
