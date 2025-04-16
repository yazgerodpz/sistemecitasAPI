using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemecitasAPI.Dto;
using sistemecitasAPI.Models;

namespace sistemecitasAPI.Controllers
{
    public class EmpleadoController : ControllerBase
    {
        private SistemaCitasContext _context;

        public EmpleadoController(SistemaCitasContext context)
        {
            _context = context;
        }

        // GET: api/Empleados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoDto>>> GetEmpleados()
        {
            var empleados = await _context.Empleados
                .Select(e => new EmpleadoDto
                {
                    IdEmpleado = e.IdEmpleado,
                    Nombre = e.Nombre,
                    Especialidad = e.Especialidad,
                    Telefono = e.Telefono,
                    CorreoElectronico = e.CorreoElectronico,
                    Activo = e.Activo
                })
                .ToListAsync();

            return Ok(empleados);
        }

        // GET: api/Empleados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoDto>> GetEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
                return NotFound();

            var dto = new EmpleadoDto
            {
                IdEmpleado = empleado.IdEmpleado,
                Nombre = empleado.Nombre,
                Especialidad = empleado.Especialidad,
                Telefono = empleado.Telefono,
                CorreoElectronico = empleado.CorreoElectronico,
                Activo = empleado.Activo
            };

            return Ok(dto);
        }

        // POST: api/Empleados
        [HttpPost]
        public async Task<ActionResult<EmpleadoDto>> PostEmpleado(EmpleadoDto dto)
        {
            var empleado = new Empleado
            { 
                Nombre = dto.Nombre,
                Especialidad = dto.Especialidad,
                Telefono = dto.Telefono,
                CorreoElectronico = dto.CorreoElectronico,
                Activo = dto.Activo ?? true
            };

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            dto.IdEmpleado = empleado.IdEmpleado;
            return CreatedAtAction(nameof(GetEmpleado), new { id = dto.IdEmpleado }, dto);
        }

        // PUT: api/Empleados/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, EmpleadoDto dto)
        {
            if (id != dto.IdEmpleado)
                return BadRequest();

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
                return NotFound();

            empleado.Nombre = dto.Nombre;
            empleado.Especialidad = dto.Especialidad;
            empleado.Telefono = dto.Telefono;
            empleado.CorreoElectronico = dto.CorreoElectronico;
            empleado.Activo = dto.Activo;

            _context.Entry(empleado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Empleados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
                return NotFound();

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
