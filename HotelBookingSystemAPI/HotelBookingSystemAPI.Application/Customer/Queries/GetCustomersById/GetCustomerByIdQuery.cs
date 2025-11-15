using MediatR;
using HotelBookingSystemAPI.Application.DTOs;

namespace HotelBookingSystemAPI.Application.Customer.Queries.GetCustomersById
{
    public class GetCustomerByIdQuery : IRequest<CustomerDTO?>
    {
        public int Id { get; set; }
    }
}
