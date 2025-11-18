using MediatR;

namespace HotelBookingSystemAPI.Application.Review.Commands.UpdateReview
{
    public class UpdateReviewCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime ReviewDate { get; set; }
    }
}