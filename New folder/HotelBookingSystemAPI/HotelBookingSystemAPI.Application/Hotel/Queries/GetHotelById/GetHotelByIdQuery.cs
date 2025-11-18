using MediatR;
using HotelBookingSystemAPI.Application.DTOs;

namespace HotelBookingSystemAPI.Application.Hotel.Queries.GetHotelById
{
    public class GetHotelByIdQuery : IRequest<HotelDTO?>
    {
        public int Id { get; set; }
    }
}