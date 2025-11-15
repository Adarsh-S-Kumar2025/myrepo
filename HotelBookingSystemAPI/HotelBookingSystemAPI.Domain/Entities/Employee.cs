using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PasswordHash { get; set; }
        public Hotel Hotel { get; set; } = null!;
    }
}
