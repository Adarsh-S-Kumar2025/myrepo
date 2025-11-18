using MediatR;

namespace HotelBookingSystemAPI.Application.Booking.Commands.DeleteBooking
{
    public class DeleteBookingCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}