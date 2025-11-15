using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Room.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, int>
    {
        private readonly HotelBookingDbContext _context;
        public CreateRoomCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = new Domain.Entities.Room
            {
                RoomNumber = request.RoomNumber,
                HotelId = request.HotelId,
                RoomTypeId = request.RoomTypeId,
                Status = request.Status,
                PricePerNight = request.PricePerNight
            };

            _context.Rooms.Add(room);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}