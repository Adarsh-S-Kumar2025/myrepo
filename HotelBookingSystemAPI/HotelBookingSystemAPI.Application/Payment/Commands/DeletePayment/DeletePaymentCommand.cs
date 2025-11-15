using MediatR;

namespace HotelBookingSystemAPI.Application.Payment.Commands.DeletePayment
{
    public class DeletePaymentCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}