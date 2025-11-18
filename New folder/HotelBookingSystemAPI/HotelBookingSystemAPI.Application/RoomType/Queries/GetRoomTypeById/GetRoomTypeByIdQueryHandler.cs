using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.RoomType.Queries.GetRoomTypeById
{
    public class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomTypeByIdQuery, RoomTypeDTO?>
    {
        private readonly HotelBookingDbContext _context;
        public GetRoomTypeByIdQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<RoomTypeDTO?> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.RoomTypes
                .AsNoTracking()
                .Where(rt => rt.Id == request.Id)
                .Select(rt => new RoomTypeDTO
                {
                    Id = rt.Id,
                    TypeName = rt.TypeName,
                    Description = rt.Description,
                    Capacity = rt.Capacity
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}