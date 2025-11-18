using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Review.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, int>
    {
        private readonly HotelBookingDbContext _context;

        public UpdateReviewCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _context.Reviews.FindAsync(new object[] { request.Id }, cancellationToken);
            if (review == null) return 0;

            review.HotelId = request.HotelId;
            review.CustomerId = request.CustomerId;
            review.Rating = request.Rating;
            review.Comment = request.Comment;
            review.ReviewDate = request.ReviewDate;

            _context.Reviews.Update(review);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}