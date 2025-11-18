using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Booking.Commands.UpdateBooking
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, int>
    {
        private readonly HotelBookingDbContext _context;

        public UpdateBookingCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings.FindAsync(new object[] { request.Id }, cancellationToken);
            if (booking == null)
                return 0;

            booking.CustomerId = request.CustomerId;
            booking.RoomId = request.RoomId;
            booking.CheckInDate = request.CheckInDate;
            booking.CheckOutDate = request.CheckOutDate;
            booking.Status = request.Status;
            booking.TotalAmount = request.TotalAmount;

            _context.Bookings.Update(booking);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}