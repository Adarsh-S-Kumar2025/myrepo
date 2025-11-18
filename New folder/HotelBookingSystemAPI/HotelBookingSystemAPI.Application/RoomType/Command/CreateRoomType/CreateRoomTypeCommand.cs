using MediatR;

namespace HotelBookingSystemAPI.Application.RoomType.Command.CreateRoomType
{
    public class CreateRoomTypeCommand : IRequest<int>
    {
        public string TypeName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
    }
}