using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Booking.Queries.GetBookingById
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDTO?>
    {
        private readonly HotelBookingDbContext _context;

        public GetBookingByIdQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<BookingDTO?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Where(b => b.Id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}