using Microsoft.AspNetCore.Mvc;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.Auth;
using HotelBookingSystemAPI.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystemAPI.Domain.Entities;

namespace HotelBookingSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HotelBookingDbContext _db;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<Employee> _hasher = new();

        public AuthController(HotelBookingDbContext db, ITokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var employee = await _db.Employees.SingleOrDefaultAsync(e => e.Email == request.Email);
            if (employee == null) return Unauthorized();

            var result = _hasher.VerifyHashedPassword(employee, employee.PasswordHash ?? string.Empty, request.Password);
            if (result == PasswordVerificationResult.Failed) return Unauthorized();

            var token = _tokenService.CreateToken(employee); // Changed from GenerateToken to CreateToken
            return Ok(new { token });
        }
    }
}