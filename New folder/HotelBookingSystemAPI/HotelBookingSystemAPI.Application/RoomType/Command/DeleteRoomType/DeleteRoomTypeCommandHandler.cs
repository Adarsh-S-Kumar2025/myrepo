using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.RoomType.Command.DeleteRoomType
{
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, int>
    {
        private readonly HotelBookingDbContext _context;
        public DeleteRoomTypeCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomType = await _context.RoomTypes.FindAsync(new object[] { request.Id }, cancellationToken);
            if (roomType == null) return 0;

            _context.RoomTypes.Remove(roomType);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}