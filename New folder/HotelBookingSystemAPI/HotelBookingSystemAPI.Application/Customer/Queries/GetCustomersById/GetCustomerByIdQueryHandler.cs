using MediatR;
using HotelBookingSystemAPI.Application.DTOs;
using HotelBookingSystemAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Customer.Queries.GetCustomersById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDTO?>
    {
        private readonly HotelBookingDbContext _context;

        public GetCustomerByIdQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<CustomerDTO?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .AsNoTracking()
                .Where(c => c.Id == request.Id)
                .Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    IdProofNumber = c.IdProofNumber
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
