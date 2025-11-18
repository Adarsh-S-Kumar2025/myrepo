using MediatR;
using Microsoft.AspNetCore.Mvc;
using HotelBookingSystemAPI.Application.DTOs;
using HotelBookingSystemAPI.Application.RoomType.Command.CreateRoomType;
using HotelBookingSystemAPI.Application.RoomType.Command.UpdateRoomType;
using HotelBookingSystemAPI.Application.RoomType.Command.DeleteRoomType;
using HotelBookingSystemAPI.Application.RoomType.Queries.GetRoomTypes;
using HotelBookingSystemAPI.Application.RoomType.Queries.GetRoomTypeById;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoomTypeController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<RoomTypeDTO>>> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRoomTypesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomTypeDTO?>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRoomTypeByIdQuery { Id = id }, cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateRoomTypeCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoomTypeCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest();
            var result = await _mediator.Send(command, cancellationToken);
            if (result == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteRoomTypeCommand { Id = id }, cancellationToken);
            if (result == 0) return NotFound();
            return NoContent();
        }
    }
}