using System;
using System.Collections.Generic;
using HotelBookingSystemAPI.Application.DTOs;
using MediatR;

namespace HotelBookingSystemAPI.Application.Room.Queries.GetAvailableRooms
{
    public class GetAvailableRoomQuery : IRequest<List<RoomDTO>>
    {
        public int HotelId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
