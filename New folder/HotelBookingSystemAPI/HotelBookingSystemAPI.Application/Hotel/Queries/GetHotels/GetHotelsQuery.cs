using MediatR;
using HotelBookingSystemAPI.Application.DTOs;
using System.Collections.Generic;

namespace HotelBookingSystemAPI.Application.Hotel.Queries.GetHotels
{
    public class GetHotelsQuery : IRequest<List<HotelDTO>>
    {
    }
}