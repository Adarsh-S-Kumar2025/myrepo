using MediatR;

namespace HotelBookingSystemAPI.Application.Room.Commands.UpdateRoom
{
    public class UpdateRoomCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public Domain.Entities.RoomStatus Status { get; set; }
        public decimal PricePerNight { get; set; }
    }
}