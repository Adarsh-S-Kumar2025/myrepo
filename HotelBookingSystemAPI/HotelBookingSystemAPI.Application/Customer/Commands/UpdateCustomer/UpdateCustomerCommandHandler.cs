using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
    {
        private readonly HotelBookingDbContext _context;

        public UpdateCustomerCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FindAsync(new object[] { request.Id }, cancellationToken);
            if (customer == null) return 0;

            customer.FullName = request.FullName;
            customer.Email = request.Email;
            customer.PhoneNumber = request.PhoneNumber;
            // Customer entity uses IdProofNumber (capital P) — map from request.IdproofNumber
            customer.IdProofNumber = request.IdproofNumber;

            _context.Customers.Update(customer);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
