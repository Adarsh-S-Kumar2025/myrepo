using MediatR;
using HotelBookingSystemAPI.Application.DTOs;

namespace HotelBookingSystemAPI.Application.Review.Queries.GetReviewById
{
    public class GetReviewByIdQuery : IRequest<ReviewDTO?>
    {
        public int Id { get; set; }
    }
}