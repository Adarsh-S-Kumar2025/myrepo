using System;
using HotelBookingSystemAPI.Domain.Entities;

namespace HotelBookingSystemAPI.Application.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
