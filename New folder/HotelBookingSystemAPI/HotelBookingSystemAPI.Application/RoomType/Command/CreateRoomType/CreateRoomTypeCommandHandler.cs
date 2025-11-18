using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.RoomType.Command.CreateRoomType
{
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, int>
    {
        private readonly HotelBookingDbContext _context;
        public CreateRoomTypeCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var roomType = new Domain.Entities.RoomType
            {
                TypeName = request.TypeName,
                Description = request.Description,
                Capacity = request.Capacity
            };

            _context.RoomTypes.Add(roomType);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}