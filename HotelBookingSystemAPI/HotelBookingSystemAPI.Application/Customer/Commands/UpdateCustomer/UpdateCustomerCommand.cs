using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HotelBookingSystemAPI.Application.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        // Keep same name as CreateCustomerCommand to remain consistent with existing callers
        public string IdproofNumber { get; set; } = null!;
    }
}
