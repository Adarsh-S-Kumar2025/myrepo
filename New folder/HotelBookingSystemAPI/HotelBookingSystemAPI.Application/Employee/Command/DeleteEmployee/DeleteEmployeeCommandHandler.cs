using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Employee.Command.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, int>
    {
        private readonly HotelBookingDbContext _context;

        public DeleteEmployeeCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(new object[] { request.Id }, cancellationToken);
            if (employee == null)
                return 0;

            _context.Employees.Remove(employee);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}