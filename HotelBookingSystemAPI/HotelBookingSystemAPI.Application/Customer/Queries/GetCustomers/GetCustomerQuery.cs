using MediatR;
using HotelBookingSystemAPI.Application.DTOs;
using System.Collections.Generic;

namespace HotelBookingSystemAPI.Application.Customer.Queries.GetCustomers
{
    public class GetCustomersQuery : IRequest<List<CustomerDTO>>
    {
    }
}
