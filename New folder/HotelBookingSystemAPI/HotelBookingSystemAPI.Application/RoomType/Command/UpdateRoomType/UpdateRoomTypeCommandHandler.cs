using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.RoomType.Command.UpdateRoomType
{
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, int>
    {
        private readonly HotelBookingDbContext _context;
        public UpdateRoomTypeCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomType = await _context.RoomTypes.FindAsync(new object[] { request.Id }, cancellationToken);
            if (roomType == null) return 0;

            roomType.TypeName = request.TypeName;
            roomType.Description = request.Description;
            roomType.Capacity = request.Capacity;

            _context.RoomTypes.Update(roomType);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}