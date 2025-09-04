using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIdbConection.Models;
using APIdbConection.Models.Catalogos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using APIdbConection.Models.Login;

namespace APIdbConection.Controllers.Catalogos
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuariosController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarios(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // GET: api/Usuarios/ExisteUsuario
        [HttpGet("ExisteUsuario")]
        public async Task<ActionResult<bool>> ExisteUsuario(string correo)
        {
            var existe = await _context.Usuarios
                .AnyAsync(u => u.Email == correo);

            return Ok(existe);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(int id, Usuarios usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            // Obtener la contraseña actual almacenada
            var usuarioActual = await _context.Usuarios.AsNoTracking()
                                   .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuarioActual == null)
                return NotFound();

            // Si la contraseña enviada no es el hash actual, asumimos que la cambió y hay que hashearla
            if (usuario.Contrasena != usuarioActual.Contrasena)
            {
                usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuario)
        {
            // Hashear la contraseña antes de guardar
            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarios", new { id = usuario.IdUsuario }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarios(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }



        // POST: api/Usuarios/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario == login.Usuario);

            if (usuario == null)
                return Unauthorized("Usuario no encontrado");

            // Verificar la contraseña con el hash guardado
            bool contrasenaValida = BCrypt.Net.BCrypt.Verify(login.Contrasena, usuario.Contrasena);

            if (!contrasenaValida)
                return Unauthorized("Contraseña incorrecta");

            // Generar el token JWT si las credenciales son correctas
            var token = GenerateJwtToken(usuario);

            // Devolver el token junto con los datos del usuario
            return Ok(new
            {
                usuario.IdUsuario,
                usuario.Usuario,
                usuario.Rol,
                usuario.Nombre,
                Token = token  // Incluye el token generado
            });
        }

        private string GenerateJwtToken(Usuarios usuario)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            // Define los claims del usuario
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Usuario), // Usuario como "Subject"
                new Claim(ClaimTypes.Name, usuario.Nombre),              // Nombre del usuario
                new Claim(ClaimTypes.Role, usuario.Rol),                 // Rol del usuario
                new Claim("UserId", usuario.IdUsuario.ToString())       // IdUsuario personalizado
            };

            // Clave secreta para firmar el token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crea el token
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],                         // Emisor del token
                audience: jwtSettings["Audience"],                     // Público objetivo del token
                claims: claims,                                        // Claims definidos arriba
                expires: DateTime.Now.AddHours(4),                      // Expiración del token
                signingCredentials: creds                              // Credenciales para firmar el token
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // Devuelve el token como string
        }


        // POST: api/Usuarios/ValidarContrasena
        [HttpPost("ValidarContrasenaActual")]
        public async Task<ActionResult<bool>> ValidarContrasenaActual([FromBody] ValidarContrasenaRequest request)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == request.IdUsuario);

            if (usuario == null)
                return NotFound("Usuario no encontrado");

            bool contrasenaValida = BCrypt.Net.BCrypt.Verify(request.ContrasenaActual, usuario.Contrasena);

            return Ok(contrasenaValida);
        }

    }
}
