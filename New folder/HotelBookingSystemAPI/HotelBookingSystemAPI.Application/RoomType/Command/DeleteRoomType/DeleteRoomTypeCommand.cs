using MediatR;

namespace HotelBookingSystemAPI.Application.RoomType.Command.DeleteRoomType
{
    public class DeleteRoomTypeCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}