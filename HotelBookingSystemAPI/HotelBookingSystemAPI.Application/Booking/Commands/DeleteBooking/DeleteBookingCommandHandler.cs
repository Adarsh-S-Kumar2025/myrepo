using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Booking.Commands.DeleteBooking
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, int>
    {
        private readonly HotelBookingDbContext _context;

        public DeleteBookingCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings.FindAsync(new object[] { request.Id }, cancellationToken);
            if (booking == null)
                return 0;

            _context.Bookings.Remove(booking);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}