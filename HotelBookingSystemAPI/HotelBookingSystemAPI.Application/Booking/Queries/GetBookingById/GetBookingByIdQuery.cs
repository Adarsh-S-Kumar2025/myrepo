using MediatR;
using HotelBookingSystemAPI.Application.DTOs;

namespace HotelBookingSystemAPI.Application.Booking.Queries.GetBookingById
{
    public class GetBookingByIdQuery : IRequest<BookingDTO?>
    {
        public int Id { get; set; }
    }
}