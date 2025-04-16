using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemecitasAPI.Dto;
using sistemecitasAPI.Models;

namespace sistemecitasAPI.Controllers
{
    public class ServicioController : ControllerBase
    {
        private SistemaCitasContext _context;

        public ServicioController(SistemaCitasContext context)
        {
            _context = context;
        }

        // GET: api/Servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicioDto>>> GetServicios()
        {
            return await _context.Servicios
                .Select(s => new ServicioDto
                {
                    IdServicio = s.IdServicio,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    DuracionMinutos = s.DuracionMinutos,
                    Precio = s.Precio
                })
                .ToListAsync();
        }

        // GET: api/Servicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicioDto>> GetServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null)
                return NotFound();

            var dto = new ServicioDto
            {
                IdServicio = servicio.IdServicio,
                Nombre = servicio.Nombre,
                Descripcion = servicio.Descripcion,
                DuracionMinutos = servicio.DuracionMinutos,
                Precio = servicio.Precio
            };

            return dto;
        }

        // POST: api/Servicios
        [HttpPost]
        public async Task<ActionResult<ServicioDto>> PostServicio(ServicioDto dto)
        {
            var servicio = new Servicio
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                DuracionMinutos = dto.DuracionMinutos,
                Precio = dto.Precio
            };

            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();

            dto.IdServicio = servicio.IdServicio;

            return CreatedAtAction(nameof(GetServicio), new { id = dto.IdServicio }, dto);
        }

        // PUT: api/Servicios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicio(int id, ServicioDto dto)
        {
            if (id != dto.IdServicio)
                return BadRequest();

            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null)
                return NotFound();

            servicio.Nombre = dto.Nombre;
            servicio.Descripcion = dto.Descripcion;
            servicio.DuracionMinutos = dto.DuracionMinutos;
            servicio.Precio = dto.Precio;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Servicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null)
                return NotFound();

            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
