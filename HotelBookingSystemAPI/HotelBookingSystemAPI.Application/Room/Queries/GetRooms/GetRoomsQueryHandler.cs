using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Room.Queries.GetRooms
{
    public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, List<RoomDTO>>
    {
        private readonly HotelBookingDbContext _context;
        public GetRoomsQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<List<RoomDTO>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Rooms
                .AsNoTracking()
                .Select(r => new RoomDTO
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    HotelId = r.HotelId,
                    RoomTypeId = r.RoomTypeId,
                    Status = r.Status,
                    PricePerNight = r.PricePerNight
                })
                .ToListAsync(cancellationToken);
        }
    }
}