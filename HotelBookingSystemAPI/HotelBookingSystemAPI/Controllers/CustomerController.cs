using HotelBookingSystemAPI.Application.Customer.Commands.CreateCustomer;
using HotelBookingSystemAPI.Application.Customer.Commands.DeleteCustomer;
using HotelBookingSystemAPI.Application.Customer.Commands.UpdateCustomer;
using HotelBookingSystemAPI.Application.Customer.Queries.GetCustomers;
using HotelBookingSystemAPI.Application.Customer.Queries.GetCustomersById;
using HotelBookingSystemAPI.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCustomersQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO?>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery { Id = id }, cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest();
            var result = await _mediator.Send(command, cancellationToken);
            if (result == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand { Id = id }, cancellationToken);
            if (result == 0) return NotFound();
            return NoContent();
        }
    }
}