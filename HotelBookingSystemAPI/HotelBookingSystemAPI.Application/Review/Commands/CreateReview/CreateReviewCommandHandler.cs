using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Review.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
    {
        private readonly HotelBookingDbContext _context;

        public CreateReviewCommandHandler(HotelBookingDbContext context) => _context = context;

        public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = new Domain.Entities.Review
            {
                HotelId = request.HotelId,
                CustomerId = request.CustomerId,
                Rating = request.Rating,
                Comment = request.Comment,
                ReviewDate = request.ReviewDate
            };

            _context.Reviews.Add(review);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}