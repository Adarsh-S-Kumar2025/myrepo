using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Employee.Command.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, int>
    {
        private readonly HotelBookingDbContext _context;

        public UpdateEmployeeCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(new object[] { request.Id }, cancellationToken);
            if (employee == null)
                return 0;

            employee.HotelId = request.HotelId;
            employee.FullName = request.FullName;
            employee.Role = request.Role;
            employee.Email = request.Email;

            _context.Employees.Update(employee);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}