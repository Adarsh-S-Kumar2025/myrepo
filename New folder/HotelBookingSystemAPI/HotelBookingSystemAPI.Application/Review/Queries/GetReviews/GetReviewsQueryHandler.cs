using MediatR;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Application.Review.Queries.GetReviews
{
    public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, List<ReviewDTO>>
    {
        private readonly HotelBookingDbContext _context;

        public GetReviewsQueryHandler(HotelBookingDbContext context) => _context = context;

        public async Task<List<ReviewDTO>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Reviews
                .AsNoTracking()
                .Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    HotelId = r.HotelId,
                    CustomerId = r.CustomerId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate
                })
                .ToListAsync(cancellationToken);
        }
    }
}