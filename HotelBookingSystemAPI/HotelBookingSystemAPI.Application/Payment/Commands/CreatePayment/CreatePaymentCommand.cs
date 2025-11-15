using System;
using MediatR;
using HotelBookingSystemAPI.Domain.Entities;

namespace HotelBookingSystemAPI.Application.Payment.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<int>
    {
        public int BookingId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
    }
}