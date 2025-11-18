using MediatR;

namespace HotelBookingSystemAPI.Application.Review.Commands.DeleteReview
{
    public class DeleteReviewCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}