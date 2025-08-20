using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace ProdutosCIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.Users.AddAsync(user);

            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);

        }
    }
}
