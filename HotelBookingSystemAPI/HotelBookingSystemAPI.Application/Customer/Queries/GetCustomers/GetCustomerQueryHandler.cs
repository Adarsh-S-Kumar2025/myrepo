using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystemAPI.Application.Customer.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerDTO>>
    {
        private readonly HotelBookingDbContext _context;
        public GetCustomersQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<List<CustomerDTO>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .AsNoTracking()
                .Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    IdProofNumber = c.IdProofNumber
                })
                .ToListAsync(cancellationToken);
        }
    }
}
