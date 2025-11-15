using MediatR;
using HotelBookingSystemAPI.Application.DTOs;
using System.Collections.Generic;

namespace HotelBookingSystemAPI.Application.Room.Queries.GetRooms
{
    public class GetRoomsQuery : IRequest<List<RoomDTO>>
    {
    }
}