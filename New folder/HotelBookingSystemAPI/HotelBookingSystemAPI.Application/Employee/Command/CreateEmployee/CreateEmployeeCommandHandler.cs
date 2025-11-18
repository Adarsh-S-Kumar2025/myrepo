using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HotelBookingSystemAPI.Domain.Entities;
using EmployeeEntity = HotelBookingSystemAPI.Domain.Entities.Employee;

namespace HotelBookingSystemAPI.Application.Employee.Command.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly HotelBookingDbContext _context;
        private readonly PasswordHasher<EmployeeEntity> _hasher = new();

        public CreateEmployeeCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new EmployeeEntity
            {
                HotelId = request.HotelId,
                FullName = request.FullName,
                Role = request.Role,
                Email = request.Email
            };

            _context.Employees.Add(employee);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}