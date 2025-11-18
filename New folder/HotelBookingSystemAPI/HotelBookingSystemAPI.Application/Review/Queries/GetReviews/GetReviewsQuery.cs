using MediatR;
using HotelBookingSystemAPI.Application.DTOs;
using System.Collections.Generic;

namespace HotelBookingSystemAPI.Application.Review.Queries.GetReviews
{
    public class GetReviewsQuery : IRequest<List<ReviewDTO>>
    {
    }
}