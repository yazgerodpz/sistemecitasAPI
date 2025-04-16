using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemecitasAPI.Dto;
using sistemecitasAPI.Models;

namespace sistemecitasAPI.Controllers
{
    public class HorariosEmpleadoController : ControllerBase
    {
        private SistemaCitasContext _context;

        public HorariosEmpleadoController(SistemaCitasContext context)
        {
            _context = context;
        }

        // GET: api/HorariosEmpleado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorariosEmpleadoDto>>> GetHorarios()
        {
            var horarios = await _context.HorariosEmpleados
                .Select(h => new HorariosEmpleadoDto
                {
                    IdHorario = h.IdHorario,
                    IdEmpleado = h.IdEmpleado,
                    DiaSemana = h.DiaSemana,
                    HoraInicio = h.HoraInicio,
                    HoraFin = h.HoraFin
                }).ToListAsync();

            return Ok(horarios);
        }

        // GET: api/HorariosEmpleado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorariosEmpleadoDto>> GetHorario(int id)
        {
            var horario = await _context.HorariosEmpleados.FindAsync(id);

            if (horario == null)
                return NotFound();

            var dto = new HorariosEmpleadoDto
            {
                IdHorario = horario.IdHorario,
                IdEmpleado = horario.IdEmpleado,
                DiaSemana = horario.DiaSemana,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin
            };

            return Ok(dto);
        }

        // POST: api/HorariosEmpleado
        [HttpPost]
        public async Task<ActionResult<HorariosEmpleadoDto>> PostHorario(HorariosEmpleadoDto dto)
        {
            var horario = new HorariosEmpleado
            {
                IdEmpleado = dto.IdEmpleado,
                DiaSemana = dto.DiaSemana,
                HoraInicio = dto.HoraInicio,
                HoraFin = dto.HoraFin
            };

            _context.HorariosEmpleados.Add(horario);
            await _context.SaveChangesAsync();

            dto.IdHorario = horario.IdHorario;

            return CreatedAtAction(nameof(GetHorario), new { id = dto.IdHorario }, dto);
        }

        // PUT: api/HorariosEmpleado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, HorariosEmpleadoDto dto)
        {
            if (id != dto.IdHorario)
                return BadRequest();

            var horario = await _context.HorariosEmpleados.FindAsync(id);
            if (horario == null)
                return NotFound();

            horario.IdEmpleado = dto.IdEmpleado;
            horario.DiaSemana = dto.DiaSemana;
            horario.HoraInicio = dto.HoraInicio;
            horario.HoraFin = dto.HoraFin;

            _context.Entry(horario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/HorariosEmpleado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var horario = await _context.HorariosEmpleados.FindAsync(id);
            if (horario == null)
                return NotFound();

            _context.HorariosEmpleados.Remove(horario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
