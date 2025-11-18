using MediatR;
using HotelBookingSystemAPI.Application.DTOs;
using System.Collections.Generic;

namespace HotelBookingSystemAPI.Application.Booking.Queries.GetBookings
{
    public class GetBookingsQuery : IRequest<List<BookingDTO>>
    {
    }
}