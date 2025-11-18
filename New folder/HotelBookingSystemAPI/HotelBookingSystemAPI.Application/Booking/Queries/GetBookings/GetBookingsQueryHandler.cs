using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Booking.Queries.GetBookings
{
    public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, List<BookingDTO>>
    {
        private readonly HotelBookingDbContext _context;

        public GetBookingsQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<List<BookingDTO>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Select(b => new BookingDTO
                {
                    Id = b.Id,
                    CustomerId = b.CustomerId,
                    RoomId = b.RoomId,
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    Status = b.Status,
                    TotalAmount = b.TotalAmount,
                    PaymentId = b.Payment != null ? b.Payment.Id : (int?)null
                })
                .ToListAsync(cancellationToken);
        }
    }
}