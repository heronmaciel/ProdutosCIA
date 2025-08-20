using Application.DTOs;
using Application.Interfaces;
using Application.Repository;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ProdutosCIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _context;

        public UsersController(IUserRepository userRepository, AppDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = createUserDto.Username,
                Role = createUserDto.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);

        }
    }
}
