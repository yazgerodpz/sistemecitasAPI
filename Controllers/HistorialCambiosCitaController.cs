using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemecitasAPI.Dto;
using sistemecitasAPI.Models;

namespace sistemecitasAPI.Controllers
{
    public class HistorialCambiosCitaController : ControllerBase
    {
        private SistemaCitasContext _context;

        public HistorialCambiosCitaController(SistemaCitasContext context)
        {
            _context = context;
        }

        // GET: api/HistorialCambiosCita
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialCambiosCitaDto>>> GetHistorial()
        {
            var historial = await _context.HistorialCambiosCitas
                .Select(h => new HistorialCambiosCitaDto
                {
                    IdHistorial = h.IdHistorial,
                    IdCita = h.IdCita,
                    FechaCambio = h.FechaCambio,
                    DescripcionCambio = h.DescripcionCambio
                }).ToListAsync();

            return Ok(historial);
        }

        // GET: api/HistorialCambiosCita/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialCambiosCitaDto>> GetHistorial(int id)
        {
            var h = await _context.HistorialCambiosCitas.FindAsync(id);

            if (h == null)
                return NotFound();

            var dto = new HistorialCambiosCitaDto
            {
                IdHistorial = h.IdHistorial,
                IdCita = h.IdCita,
                FechaCambio = h.FechaCambio,
                DescripcionCambio = h.DescripcionCambio
            };

            return Ok(dto);
        }

        // POST: api/HistorialCambiosCita
        [HttpPost]
        public async Task<ActionResult<HistorialCambiosCitaDto>> PostHistorial(HistorialCambiosCitaDto dto)
        {
            var historial = new HistorialCambiosCita
            {
                IdCita = dto.IdCita,
                FechaCambio = dto.FechaCambio ?? DateTime.Now,
                DescripcionCambio = dto.DescripcionCambio
            };

            _context.HistorialCambiosCitas.Add(historial);
            await _context.SaveChangesAsync();

            dto.IdHistorial = historial.IdHistorial;

            return CreatedAtAction(nameof(GetHistorial), new { id = dto.IdHistorial }, dto);
        }

        // PUT: api/HistorialCambiosCita/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorial(int id, HistorialCambiosCitaDto dto)
        {
            if (id != dto.IdHistorial)
                return BadRequest();

            var historial = await _context.HistorialCambiosCitas.FindAsync(id);
            if (historial == null)
                return NotFound();

            historial.IdCita = dto.IdCita;
            historial.FechaCambio = dto.FechaCambio ?? DateTime.Now;
            historial.DescripcionCambio = dto.DescripcionCambio;

            _context.Entry(historial).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/HistorialCambiosCita/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorial(int id)
        {
            var historial = await _context.HistorialCambiosCitas.FindAsync(id);
            if (historial == null)
                return NotFound();

            _context.HistorialCambiosCitas.Remove(historial);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
