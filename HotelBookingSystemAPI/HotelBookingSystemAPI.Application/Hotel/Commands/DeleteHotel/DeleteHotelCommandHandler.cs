using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Hotel.Commands.DeleteHotel
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, int>
    {
        private readonly HotelBookingDbContext _context;
        public DeleteHotelCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.FindAsync(new object[] { request.Id }, cancellationToken);
            if (hotel == null) return 0;

            _context.Hotels.Remove(hotel);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}