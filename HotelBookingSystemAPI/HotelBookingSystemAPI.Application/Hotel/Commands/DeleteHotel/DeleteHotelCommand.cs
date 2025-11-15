using MediatR;

namespace HotelBookingSystemAPI.Application.Hotel.Commands.DeleteHotel
{
    public class DeleteHotelCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}