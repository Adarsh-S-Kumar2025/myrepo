using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystemAPI.Application.Payment.Queries.GetPayments
{
    public class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, List<PaymentDTO>>
    {
        private readonly HotelBookingDbContext _context;

        public GetPaymentsQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<List<PaymentDTO>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Payments
                .AsNoTracking()
                .Select(p => new PaymentDTO
                {
                    Id = p.Id,
                    BookingId = p.BookingId,
                    PaymentDate = p.PaymentDate,
                    Amount = p.Amount,
                    Method = p.Method,
                    Status = p.Status
                })
                .ToListAsync(cancellationToken);
        }
    }
}