using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Review.Queries.GetReviewById
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDTO?>
    {
        private readonly HotelBookingDbContext _context;

        public GetReviewByIdQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<ReviewDTO?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Reviews
                .AsNoTracking()
                .Where(r => r.Id == request.Id)
                .Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    HotelId = r.HotelId,
                    CustomerId = r.CustomerId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}