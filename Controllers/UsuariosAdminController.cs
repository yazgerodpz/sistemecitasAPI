using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemecitasAPI.Dto;
using sistemecitasAPI.Models;

namespace sistemecitasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosAdminController : ControllerBase
    {
        private SistemaCitasContext _context;

        public  UsuariosAdminController(SistemaCitasContext context)
        {
            _context = context;
        }

        // GET: api/UsuariosAdmin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosAdminDto>>> GetUsuarios()
        {
            var usuarios = await _context.UsuariosAdmins
                .Select(u => new UsuariosAdminDto
                {
                    IdUsuario = u.IdUsuario,
                    Usuario = u.Usuario,
                    ContrasenaHash = u.ContrasenaHash,
                    Rol = u.Rol
                }).ToListAsync();

            return Ok(usuarios);
        }

        // GET: api/UsuariosAdmin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosAdminDto>> GetUsuario(int id)
        {
            var usuario = await _context.UsuariosAdmins.FindAsync(id);

            if (usuario == null)
                return NotFound();

            var dto = new UsuariosAdminDto
            {
                IdUsuario = usuario.IdUsuario,
                Usuario = usuario.Usuario,
                ContrasenaHash = usuario.ContrasenaHash,
                Rol = usuario.Rol
            };

            return Ok(dto);
        }

        // POST: api/UsuariosAdmin
        [HttpPost]
        public async Task<ActionResult<UsuariosAdminDto>> CreateUsuario(UsuariosAdminDto dto)
        {
            var usuario = new UsuariosAdmin
            {
                Usuario = dto.Usuario,
                ContrasenaHash = dto.ContrasenaHash,
                Rol = dto.Rol
            };

            _context.UsuariosAdmins.Add(usuario);
            await _context.SaveChangesAsync();

            dto.IdUsuario = usuario.IdUsuario;

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, dto);
        }

        // PUT: api/UsuariosAdmin/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, UsuariosAdminDto dto)
        {
            if (id != dto.IdUsuario)
                return BadRequest();

            var usuario = await _context.UsuariosAdmins.FindAsync(id);
            if (usuario == null)
                return NotFound();

            usuario.Usuario = dto.Usuario;
            usuario.ContrasenaHash = dto.ContrasenaHash;
            usuario.Rol = dto.Rol;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/UsuariosAdmin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.UsuariosAdmins.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _context.UsuariosAdmins.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
