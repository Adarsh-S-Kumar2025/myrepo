using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Employee.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO?>
    {
        private readonly HotelBookingDbContext _context;

        public GetEmployeeByIdQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<EmployeeDTO?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .AsNoTracking()
                .Where(e => e.Id == request.Id)
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    HotelId = e.HotelId,
                    FullName = e.FullName,
                    Role = e.Role,
                    Email = e.Email
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}