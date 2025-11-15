using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HotelBookingSystemAPI.Infrastructure.Data;

namespace HotelBookingSystemAPI.Application.Payment.Commands.UpdatePayment
{
    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, int>
    {
        private readonly HotelBookingDbContext _context;

        public UpdatePaymentCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments.FindAsync(new object[] { request.Id }, cancellationToken);
            if (payment == null) return 0;

            payment.BookingId = request.BookingId;
            payment.PaymentDate = request.PaymentDate;
            payment.Amount = request.Amount;
            payment.Method = request.Method;
            payment.Status = request.Status;

            _context.Payments.Update(payment);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}