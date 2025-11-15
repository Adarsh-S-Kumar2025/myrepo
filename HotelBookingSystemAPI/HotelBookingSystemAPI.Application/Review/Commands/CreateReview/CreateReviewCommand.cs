using MediatR;

namespace HotelBookingSystemAPI.Application.Review.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<int>
    {
        public int HotelId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime ReviewDate { get; set; }
    }
}