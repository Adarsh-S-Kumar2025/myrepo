using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Room.Queries.GetRoomById
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, RoomDTO?>
    {
        private readonly HotelBookingDbContext _context;
        public GetRoomByIdQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<RoomDTO?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Rooms
                .AsNoTracking()
                .Where(r => r.Id == request.Id)
                .Select(r => new RoomDTO
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    HotelId = r.HotelId,
                    RoomTypeId = r.RoomTypeId,
                    Status = r.Status,
                    PricePerNight = r.PricePerNight
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}