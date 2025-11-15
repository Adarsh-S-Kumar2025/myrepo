using MediatR;

namespace HotelBookingSystemAPI.Application.Employee.Command.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}