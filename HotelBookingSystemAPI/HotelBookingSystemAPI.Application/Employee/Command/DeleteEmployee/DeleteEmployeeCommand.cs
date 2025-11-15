using MediatR;

namespace HotelBookingSystemAPI.Application.Employee.Command.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}