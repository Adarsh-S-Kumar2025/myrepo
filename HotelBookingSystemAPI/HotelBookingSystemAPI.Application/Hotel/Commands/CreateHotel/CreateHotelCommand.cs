using MediatR;

namespace HotelBookingSystemAPI.Application.Hotel.Commands.CreateHotel
{
    public class CreateHotelCommand : IRequest<int>
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}