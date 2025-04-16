using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sistemecitasAPI.Dto;
using sistemecitasAPI.Models;

namespace sistemecitasAPI.Controllers
{
    public class ClienteController : Controller
    {
        private SistemaCitasContext _context;

        public ClienteController(SistemaCitasContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetClientes()
        {
            var clientes = await _context.Clientes
                .Select(c => new ClienteDto
                {
                    IdCliente = c.IdCliente,
                    Nombre = c.Nombre,
                    Apellidos = c.Apellidos,
                    Telefono = c.Telefono,
                    CorreoElectronico = c.CorreoElectronico,
                    FechaRegistro = c.FechaRegistro
                })
                .ToListAsync();

            return Ok(clientes);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            var dto = new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                Telefono = cliente.Telefono,
                CorreoElectronico = cliente.CorreoElectronico,
                FechaRegistro = cliente.FechaRegistro
            };

            return Ok(dto);
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<ClienteDto>> PostCliente(ClienteDto dto)
        {
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Apellidos = dto.Apellidos,
                Telefono = dto.Telefono,
                CorreoElectronico = dto.CorreoElectronico,
                FechaRegistro = dto.FechaRegistro ?? DateTime.Now
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            dto.IdCliente = cliente.IdCliente;
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, dto);
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteDto dto)
        {
            if (id != dto.IdCliente)
                return BadRequest();

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            cliente.Nombre = dto.Nombre;
            cliente.Apellidos = dto.Apellidos;
            cliente.Telefono = dto.Telefono;
            cliente.CorreoElectronico = dto.CorreoElectronico;
            cliente.FechaRegistro = dto.FechaRegistro;

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
