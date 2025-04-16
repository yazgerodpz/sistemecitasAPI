using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemecitasAPI.Dto;
using sistemecitasAPI.Models;

namespace sistemecitasAPI.Controllers
{
    public class ServiciosController : ControllerBase
    {
        private SistemaCitasContext _context;

        public ServiciosController(SistemaCitasContext context)
        {
            _context = context;
        }

        // GET: api/Servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicioDto>>> GetServicios()
        {
            var servicios = await _context.Servicios
                .Select(s => new ServicioDto
                {
                    IdServicio = s.IdServicio,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    DuracionMinutos = s.DuracionMinutos,
                    Precio = s.Precio
                }).ToListAsync();

            return Ok(servicios);
        }

        // GET: api/Servicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicioDto>> GetServicio(int id)
        {
            var servicio = await _context.Servicios
                .Where(s => s.IdServicio == id)
                .Select(s => new ServicioDto
                {
                    IdServicio = s.IdServicio,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    DuracionMinutos = s.DuracionMinutos,
                    Precio = s.Precio
                })
                .FirstOrDefaultAsync();

            if (servicio == null)
            {
                return NotFound();
            }

            return Ok(servicio);
        }

        // POST: api/Servicios
        [HttpPost]
        public async Task<ActionResult<ServicioDto>> PostServicio(ServicioDto servicioDto)
        {
            var servicio = new Servicio
            {
                Nombre = servicioDto.Nombre,
                Descripcion = servicioDto.Descripcion,
                DuracionMinutos = servicioDto.DuracionMinutos,
                Precio = servicioDto.Precio
            };

            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();

            servicioDto.IdServicio = servicio.IdServicio;

            return CreatedAtAction(nameof(GetServicio), new { id = servicio.IdServicio }, servicioDto);
        }

        // PUT: api/Servicios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicio(int id, ServicioDto servicioDto)
        {
            if (id != servicioDto.IdServicio)
            {
                return BadRequest();
            }

            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null)
            {
                return NotFound();
            }

            servicio.Nombre = servicioDto.Nombre;
            servicio.Descripcion = servicioDto.Descripcion;
            servicio.DuracionMinutos = servicioDto.DuracionMinutos;
            servicio.Precio = servicioDto.Precio;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Servicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }

            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
