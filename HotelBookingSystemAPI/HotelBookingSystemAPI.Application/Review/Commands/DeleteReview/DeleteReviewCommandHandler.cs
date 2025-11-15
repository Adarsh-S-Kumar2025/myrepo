using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Review.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, int>
    {
        private readonly HotelBookingDbContext _context;

        public DeleteReviewCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews.FindAsync(new object[] { request.Id }, cancellationToken);
            if (review == null) return 0;

            _context.Reviews.Remove(review);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}