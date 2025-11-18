using System;
using HotelBookingSystemAPI.Application.DTOs;
using HotelBookingSystemAPI.Application.Hotel.Commands.CreateHotel;
using HotelBookingSystemAPI.Application.Hotel.Commands.DeleteHotel;
using HotelBookingSystemAPI.Application.Hotel.Commands.UpdateHotel;
using HotelBookingSystemAPI.Application.Hotel.Queries.GetAvailableHotels;
using HotelBookingSystemAPI.Application.Hotel.Queries.GetHotelById;
using HotelBookingSystemAPI.Application.Hotel.Queries.GetHotels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotelBookingSystemAPI.Domain.Entities;

namespace HotelBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HotelController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<HotelDTO>>> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetHotelsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO?>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetHotelByIdQuery { Id = id }, cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateHotelCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateHotelCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest();
            var result = await _mediator.Send(command, cancellationToken);
            if (result == 0) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteHotelCommand { Id = id }, cancellationToken);
            if (result == 0) return NotFound();
            return NoContent();
        }


        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable(
           [FromQuery(Name = "location")] string location,
           [FromQuery(Name = "checkIn")] DateTime checkInDate,
           [FromQuery(Name = "checkOut")] DateTime checkOutDate,
           [FromQuery(Name = "price")] decimal? maxPrice, // Changed to a single 'price' parameter
           CancellationToken cancellationToken,
           [FromQuery(Name = "sortBy")] HotelSortBy sortBy = HotelSortBy.Default)
        {
            if (string.IsNullOrWhiteSpace(location))
                return BadRequest("Query parameter 'location' is required.");

            if (checkOutDate <= checkInDate)
                return BadRequest("'checkOut' must be after 'checkIn'.");

            var query = new GetAvailableHotelsQuery(
                location,
                checkInDate,
                checkOutDate,
                maxPrice, // Pass the single price value
                sortBy);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}