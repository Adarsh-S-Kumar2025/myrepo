using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Hotel.Commands.CreateHotel
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, int>
    {
        private readonly HotelBookingDbContext _context;
        public CreateHotelCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = new Domain.Entities.Hotel
            {
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                PhoneNumber = request.PhoneNumber
            };

            _context.Hotels.Add(hotel);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}