using MediatR;
using Microsoft.AspNetCore.Mvc;
using HotelBookingSystemAPI.Application.DTOs;
using HotelBookingSystemAPI.Application.Payment.Queries.GetPayments;
using HotelBookingSystemAPI.Application.Payment.Queries.GetPaymentById;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotelBookingSystemAPI.Application.Payment.Commands.UpdatePayment;
using HotelBookingSystemAPI.Application.Payment.Commands.CreatePayment;
using HotelBookingSystemAPI.Application.Payment.Commands.DeletePayment;

namespace HotelBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<PaymentDTO>>> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPaymentsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO?>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPaymentByIdQuery { Id = id }, cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest();
            var result = await _mediator.Send(command, cancellationToken);
            if (result == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeletePaymentCommand { Id = id }, cancellationToken);
            if (result == 0) return NotFound();
            return NoContent();
        }
    }
}