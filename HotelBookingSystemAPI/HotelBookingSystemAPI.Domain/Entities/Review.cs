using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime ReviewDate { get; set; }

        public Hotel Hotel { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
    }
}
