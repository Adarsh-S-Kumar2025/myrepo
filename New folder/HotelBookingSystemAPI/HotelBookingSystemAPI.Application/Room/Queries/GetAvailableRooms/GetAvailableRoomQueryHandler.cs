using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelBookingSystemAPI.Application.DTOs;
using HotelBookingSystemAPI.Domain.Entities;
using HotelBookingSystemAPI.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystemAPI.Application.Room.Queries.GetAvailableRooms
{
    public class GetAvailableRoomQueryHandler : IRequestHandler<GetAvailableRoomQuery, List<RoomDTO>>
    {
        private readonly HotelBookingDbContext _context;

        public GetAvailableRoomQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoomDTO>> Handle(GetAvailableRoomQuery request, CancellationToken cancellationToken)
        {
            var unavailableRoomIds = await _context.Bookings
                .Where(b => b.Room.HotelId == request.HotelId &&
                            (b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Pending) &&
                            b.CheckInDate < request.CheckOutDate && b.CheckOutDate > request.CheckInDate)
                .Select(b => b.RoomId)
                .Distinct()
                .ToListAsync(cancellationToken);

            var availableRooms = await _context.Rooms
                .Where(r => r.HotelId == request.HotelId &&
                            r.Status != RoomStatus.UnderMaintenance &&
                            !unavailableRoomIds.Contains(r.Id))
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

            return availableRooms;
        }
    }
}