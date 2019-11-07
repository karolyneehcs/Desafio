using Desafio.Context;
using Desafio.Extensions;
using Desafio.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly DesafioContext _context;

        public AuthController(IOptions<AppSettings> appSettings, DesafioContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(LoginUsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(a => a.Errors).Select(a => a.ErrorMessage));
            }

            var user = _context.Users.FirstOrDefault(a => a.Email.Contains(usuario.Email) && a.Senha == usuario.Senha);

            if(user != null)
            {
                return Ok(GerarJwt(user.Email));
            }

            return BadRequest(new[] { "Usuário ou senha inválido" });
        }

        private object GerarJwt(string email)
        {
            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaim(new Claim("Email", email));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor 
            {
                Issuer = _appSettings.Emitter,
                Audience = _appSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.HourValid),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return new             
            {
                TokenAcesso = encodedToken,
                ExpiraEm = TimeSpan.FromHours(_appSettings.HourValid).TotalSeconds,
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}