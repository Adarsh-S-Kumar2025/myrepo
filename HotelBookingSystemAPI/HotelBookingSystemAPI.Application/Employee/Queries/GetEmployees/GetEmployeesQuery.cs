using MediatR;
using HotelBookingSystemAPI.Application.DTOs;
using System.Collections.Generic;

namespace HotelBookingSystemAPI.Application.Employee.Queries.GetEmployees
{
    public class GetEmployeesQuery : IRequest<List<EmployeeDTO>>
    {
    }
}