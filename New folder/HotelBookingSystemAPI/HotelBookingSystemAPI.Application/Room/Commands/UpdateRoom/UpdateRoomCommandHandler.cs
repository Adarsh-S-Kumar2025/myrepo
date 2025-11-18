using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Room.Commands.UpdateRoom
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, int>
    {
        private readonly HotelBookingDbContext _context;
        public UpdateRoomCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms.FindAsync(new object[] { request.Id }, cancellationToken);
            if (room == null) return 0;

            room.RoomNumber = request.RoomNumber;
            room.HotelId = request.HotelId;
            room.RoomTypeId = request.RoomTypeId;
            room.Status = request.Status;
            room.PricePerNight = request.PricePerNight;

            _context.Rooms.Update(room);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}