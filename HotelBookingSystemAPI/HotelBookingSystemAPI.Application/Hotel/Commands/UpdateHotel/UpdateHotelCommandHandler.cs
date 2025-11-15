using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Hotel.Commands.UpdateHotel
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, int>
    {
        private readonly HotelBookingDbContext _context;
        public UpdateHotelCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.FindAsync(new object[] { request.Id }, cancellationToken);
            if (hotel == null) return 0;

            hotel.Name = request.Name;
            hotel.Address = request.Address;
            hotel.City = request.City;
            hotel.Country = request.Country;
            hotel.PhoneNumber = request.PhoneNumber;

            _context.Hotels.Update(hotel);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}