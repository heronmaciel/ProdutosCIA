using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProdutosCIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _context;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username = loginRequest.Username);

            if (user == null)
            {
                return Unauthorized("Usuário ou senha inválidos");
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, loginRequest.PasswordHash);
            if (isPasswordValid)
            {
                return Unauthorized("Usuário ou senha inválidos");
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
