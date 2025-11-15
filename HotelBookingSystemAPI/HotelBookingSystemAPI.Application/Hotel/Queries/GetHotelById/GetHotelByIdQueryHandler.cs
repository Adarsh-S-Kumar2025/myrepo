using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Hotel.Queries.GetHotelById
{
    public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, HotelDTO?>
    {
        private readonly HotelBookingDbContext _context;
        public GetHotelByIdQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<HotelDTO?> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Hotels
                .AsNoTracking()
                .Where(h => h.Id == request.Id)
                .Select(h => new HotelDTO
                {
                    Id = h.Id,
                    Name = h.Name,
                    Address = h.Address,
                    City = h.City,
                    Country = h.Country,
                    PhoneNumber = h.PhoneNumber
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}