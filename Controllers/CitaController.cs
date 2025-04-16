using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemecitasAPI.Dto;
using sistemecitasAPI.Models;

namespace sistemecitasAPI.Controllers
{
    public class CitaController : ControllerBase
    {
        private SistemaCitasContext _context;

        public CitaController(SistemaCitasContext context)
        {
            _context = context;
        }

        // GET: api/Citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetCitas()
        {
            var citas = await _context.Citas
                .Select(c => new CitaDto
                {
                    IdCita = c.IdCita,
                    IdCliente = c.IdCliente,
                    IdEmpleado = c.IdEmpleado,
                    IdServicio = c.IdServicio,
                    Fecha = c.Fecha,
                    HoraInicio = c.HoraInicio,
                    Estado = c.Estado,
                    Notas = c.Notas
                }).ToListAsync();

            return Ok(citas);
        }

        // GET: api/Citas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitaDto>> GetCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);

            if (cita == null)
                return NotFound();

            var dto = new CitaDto
            {
                IdCita = cita.IdCita,
                IdCliente = cita.IdCliente,
                IdEmpleado = cita.IdEmpleado,
                IdServicio = cita.IdServicio,
                Fecha = cita.Fecha,
                HoraInicio = cita.HoraInicio,
                Estado = cita.Estado,
                Notas = cita.Notas
            };

            return Ok(dto);
        }

        // POST: api/Citas
        [HttpPost]
        public async Task<ActionResult<CitaDto>> PostCita(CitaDto dto)
        {
            var cita = new Cita
            {
                IdCliente = dto.IdCliente,
                IdEmpleado = dto.IdEmpleado,
                IdServicio = dto.IdServicio,
                Fecha = dto.Fecha,
                HoraInicio = dto.HoraInicio,
                Estado = dto.Estado,
                Notas = dto.Notas
            };

            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();

            dto.IdCita = cita.IdCita;

            return CreatedAtAction(nameof(GetCita), new { id = dto.IdCita }, dto);
        }

        // PUT: api/Citas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, CitaDto dto)
        {
            if (id != dto.IdCita)
                return BadRequest();

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
                return NotFound();

            cita.IdCliente = dto.IdCliente;
            cita.IdEmpleado = dto.IdEmpleado;
            cita.IdServicio = dto.IdServicio;
            cita.Fecha = dto.Fecha;
            cita.HoraInicio = dto.HoraInicio;
            cita.Estado = dto.Estado;
            cita.Notas = dto.Notas;

            _context.Entry(cita).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Citas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
                return NotFound();

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
