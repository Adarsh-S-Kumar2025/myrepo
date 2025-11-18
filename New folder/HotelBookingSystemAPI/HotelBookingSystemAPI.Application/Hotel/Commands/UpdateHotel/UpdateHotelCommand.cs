using MediatR;

namespace HotelBookingSystemAPI.Application.Hotel.Commands.UpdateHotel
{
    public class UpdateHotelCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}