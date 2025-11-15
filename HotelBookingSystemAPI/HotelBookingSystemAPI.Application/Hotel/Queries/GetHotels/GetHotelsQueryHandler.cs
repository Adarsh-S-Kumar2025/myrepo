using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Hotel.Queries.GetHotels
{
    public class GetHotelsQueryHandler : IRequestHandler<GetHotelsQuery, List<HotelDTO>>
    {
        private readonly HotelBookingDbContext _context;
        public GetHotelsQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<List<HotelDTO>> Handle(GetHotelsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Hotels
                .AsNoTracking()
                .Select(h => new HotelDTO
                {
                    Id = h.Id,
                    Name = h.Name,
                    Address = h.Address,
                    City = h.City,
                    Country = h.Country,
                    PhoneNumber = h.PhoneNumber
                })
                .ToListAsync(cancellationToken);
        }
    }
}