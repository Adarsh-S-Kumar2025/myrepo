using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.RoomType.Queries.GetRoomTypes
{
    public class GetRoomTypesQueryHandler : IRequestHandler<GetRoomTypesQuery, List<RoomTypeDTO>>
    {
        private readonly HotelBookingDbContext _context;
        public GetRoomTypesQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<List<RoomTypeDTO>> Handle(GetRoomTypesQuery request, CancellationToken cancellationToken)
        {
            return await _context.RoomTypes
                .AsNoTracking()
                .Select(rt => new RoomTypeDTO
                {
                    Id = rt.Id,
                    TypeName = rt.TypeName,
                    Description = rt.Description,
                    Capacity = rt.Capacity
                })
                .ToListAsync(cancellationToken);
        }
    }
}