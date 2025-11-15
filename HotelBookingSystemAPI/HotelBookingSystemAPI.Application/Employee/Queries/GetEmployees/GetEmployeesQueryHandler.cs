using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Employee.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDTO>>
    {
        private readonly HotelBookingDbContext _context;

        public GetEmployeesQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<List<EmployeeDTO>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .AsNoTracking()
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    HotelId = e.HotelId,
                    FullName = e.FullName,
                    Role = e.Role,
                    Email = e.Email
                })
                .ToListAsync(cancellationToken);
        }
    }
}