using MediatR;
using HotelBookingSystemAPI.Application.DTOs;

namespace HotelBookingSystemAPI.Application.RoomType.Queries.GetRoomTypeById
{
    public class GetRoomTypeByIdQuery : IRequest<RoomTypeDTO?>
    {
        public int Id { get; set; }
    }
}