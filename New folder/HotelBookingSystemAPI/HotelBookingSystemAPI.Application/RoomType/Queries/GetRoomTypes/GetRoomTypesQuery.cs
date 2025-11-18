using MediatR;
using HotelBookingSystemAPI.Application.DTOs;
using System.Collections.Generic;

namespace HotelBookingSystemAPI.Application.RoomType.Queries.GetRoomTypes
{
    public class GetRoomTypesQuery : IRequest<List<RoomTypeDTO>>
    {
    }
}