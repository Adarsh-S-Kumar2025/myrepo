using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Booking.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
    {
        private readonly HotelBookingDbContext _context;

        public CreateBookingCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = new Domain.Entities.Booking
            {
                CustomerId = request.CustomerId,
                RoomId = request.RoomId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                Status = request.Status,
                TotalAmount = request.TotalAmount
            };

            _context.Bookings.Add(booking);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}