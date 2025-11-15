using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

// Add this using directive to resolve ambiguity if needed
using PaymentEntity = HotelBookingSystemAPI.Domain.Entities.Payment;

namespace HotelBookingSystemAPI.Application.Payment.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly HotelBookingDbContext _context;
        public CreatePaymentCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new PaymentEntity
            {
                BookingId = request.BookingId,
                PaymentDate = request.PaymentDate,
                Amount = request.Amount,
                Method = request.Method,
                Status = request.Status
            };

            _context.Payments.Add(payment);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}