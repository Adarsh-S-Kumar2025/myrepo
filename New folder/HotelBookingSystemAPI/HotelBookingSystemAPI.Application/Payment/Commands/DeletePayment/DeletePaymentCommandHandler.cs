using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HotelBookingSystemAPI.Infrastructure.Data;

namespace HotelBookingSystemAPI.Application.Payment.Commands.DeletePayment
{
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, int>
    {
        private readonly HotelBookingDbContext _context;

        public DeletePaymentCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments.FindAsync(new object[] { request.Id }, cancellationToken);
            if (payment == null) return 0;

            _context.Payments.Remove(payment);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}