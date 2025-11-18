using HotelBookingSystemAPI.Infrastructure.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly HotelBookingDbContext _context;

        public CreateCustomerCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Domain.Entities.Customer
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IdProofNumber = request.IdproofNumber
            };

            _context.Customers.Add(customer);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
