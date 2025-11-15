using HotelBookingSystemAPI.Application.DTOs;
using HotelBookingSystemAPI.Application.Employee.Command.CreateEmployee;
using HotelBookingSystemAPI.Application.Employee.Command.DeleteEmployee;
using HotelBookingSystemAPI.Application.Employee.Command.UpdateEmployee;
using HotelBookingSystemAPI.Application.Employee.Queries.GetEmployeeById;
using HotelBookingSystemAPI.Application.Employee.Queries.GetEmployees;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetEmployeesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO?>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery { Id = id }, cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest();
            var result = await _mediator.Send(command, cancellationToken);
            if (result == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand { Id = id }, cancellationToken);
            if (result == 0) return NotFound();
            return NoContent();
        }
    }
}