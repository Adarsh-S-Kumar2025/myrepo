using MediatR;

namespace HotelBookingSystemAPI.Application.RoomType.Command.UpdateRoomType
{
    public class UpdateRoomTypeCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
    }
}