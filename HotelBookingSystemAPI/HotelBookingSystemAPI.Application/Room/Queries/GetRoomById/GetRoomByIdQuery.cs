using MediatR;
using HotelBookingSystemAPI.Application.DTOs;

namespace HotelBookingSystemAPI.Application.Room.Queries.GetRoomById
{
    public class GetRoomByIdQuery : IRequest<RoomDTO?>
    {
        public int Id { get; set; }
    }
}