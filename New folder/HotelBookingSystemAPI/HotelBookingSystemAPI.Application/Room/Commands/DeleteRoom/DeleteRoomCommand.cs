using MediatR;

namespace HotelBookingSystemAPI.Application.Room.Commands.DeleteRoom
{
    public class DeleteRoomCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}