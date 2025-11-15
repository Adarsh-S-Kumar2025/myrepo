using MediatR;
using System;

namespace HotelBookingSystemAPI.Application.Booking.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest<int>
    {
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Domain.Entities.BookingStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}