using MediatR;
using HotelBookingSystemAPI.Application.DTOs;

namespace HotelBookingSystemAPI.Application.Payment.Queries.GetPaymentById
{
    public class GetPaymentByIdQuery : IRequest<PaymentDTO?>
    {
        public int Id { get; set; }
    }
}