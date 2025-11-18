using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Room.Commands.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, int>
    {
        private readonly HotelBookingDbContext _context;
        public DeleteRoomCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms.FindAsync(new object[] { request.Id }, cancellationToken);
            if (room == null) return 0;

            _context.Rooms.Remove(room);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}