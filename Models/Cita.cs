using System;
using System.Collections.Generic;

namespace sistemecitasAPI.Models;

public partial class Cita
{
    public int IdCita { get; set; }

    public int IdCliente { get; set; }

    public int IdEmpleado { get; set; }

    public int IdServicio { get; set; }

    public DateTime Fecha { get; set; }

    public TimeSpan HoraInicio { get; set; }

    public string? Estado { get; set; }

    public string? Notas { get; set; }

    public virtual ICollection<HistorialCambiosCita> HistorialCambiosCita { get; set; } = new List<HistorialCambiosCita>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
