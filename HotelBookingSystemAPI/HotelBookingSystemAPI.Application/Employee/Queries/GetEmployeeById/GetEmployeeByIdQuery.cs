using MediatR;
using HotelBookingSystemAPI.Application.DTOs;

namespace HotelBookingSystemAPI.Application.Employee.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDTO?>
    {
        public int Id { get; set; }
    }
}