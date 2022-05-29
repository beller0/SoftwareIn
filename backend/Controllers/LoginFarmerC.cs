
using backendAg.Helper;
using backendAg.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class LoginFarmerC : ControllerBase
    {
        private readonly integerProjectC _context;
        private readonly IConfiguration config;
        public LoginFarmerC(integerProjectC context, IConfiguration configuration)
        {
            this._context = context;
            this.config = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(LoginFa login)
        {
            //buscamos al usuario que coincida el correo
            var user = await _context.Farmers.Where(x => x.Email == login.Email).FirstOrDefaultAsync();
            if(user == null)
            {
                return NotFound( "No se ha encontrado el usuario");

            }
            if(HashHelper.CheckHash(login.Password, user.Password, user.UserName))
            {
                var secretKey = config.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);
                var userid = user.Id.ToString();
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.Email, userid));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    //Tiempo de expiracion
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);


                //bearer_token es nuestro token
                string bearer_token = tokenHandler.WriteToken(createdToken);
                return Ok(new {
                    token = bearer_token,
                    userData = new {
                        id = user.Id,
                        userName = user.UserName
                    }
                });
            }
            else
            {
                return NotFound("Contraseña no valida");
            }
        }

        //Metodo para obtener los valores del usuario activo, devuelve el correo
        [HttpGet]
        public IActionResult Get()
        {
            var r = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return Ok(r == null ? "" : "Usuario " + r.Value);
        }
    }
}
