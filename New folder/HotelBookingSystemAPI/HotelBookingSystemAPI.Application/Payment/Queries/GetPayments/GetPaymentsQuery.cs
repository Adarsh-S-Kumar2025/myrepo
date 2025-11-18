using MediatR;
using HotelBookingSystemAPI.Application.DTOs;
using System.Collections.Generic;

namespace HotelBookingSystemAPI.Application.Payment.Queries.GetPayments
{
    public class GetPaymentsQuery : IRequest<List<PaymentDTO>>
    {
    }
}