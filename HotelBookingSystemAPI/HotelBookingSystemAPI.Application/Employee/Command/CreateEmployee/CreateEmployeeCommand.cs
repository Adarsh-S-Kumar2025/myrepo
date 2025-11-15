using MediatR;

namespace HotelBookingSystemAPI.Application.Employee.Command.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<int>
    {
        public int HotelId { get; set; }
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}