using HotelBookingSystemAPI.Application.DTOs;
using HotelBookingSystemAPI.Application.Room.Commands.CreateRoom;
using HotelBookingSystemAPI.Application.Room.Commands.DeleteRoom;
using HotelBookingSystemAPI.Application.Room.Commands.UpdateRoom;
using HotelBookingSystemAPI.Application.Room.Queries.GetRoomById;
using HotelBookingSystemAPI.Application.Room.Queries.GetRooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotelBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoomController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<RoomDTO>>> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRoomsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO?>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRoomByIdQuery { Id = id }, cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateRoomCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoomCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest();
            var result = await _mediator.Send(command, cancellationToken);
            if (result == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteRoomCommand { Id = id }, cancellationToken);
            if (result == 0) return NotFound();
            return NoContent();
        }
    }
}