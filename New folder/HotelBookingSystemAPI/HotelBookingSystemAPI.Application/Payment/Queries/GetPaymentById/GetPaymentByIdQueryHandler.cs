using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelBookingSystemAPI.Application.Payment.Queries.GetPaymentById
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, PaymentDTO?>
    {
        private readonly HotelBookingDbContext _context;

        public GetPaymentByIdQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<PaymentDTO?> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Payments
                .AsNoTracking()
                .Where(p => p.Id == request.Id)
                .Select(p => new PaymentDTO
                {
                    Id = p.Id,
                    BookingId = p.BookingId,
                    PaymentDate = p.PaymentDate,
                    Amount = p.Amount,
                    Method = p.Method,
                    Status = p.Status
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}