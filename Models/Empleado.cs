using System;
using System.Collections.Generic;

namespace sistemecitasAPI.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Especialidad { get; set; }

    public string? Telefono { get; set; }

    public string? CorreoElectronico { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<HorariosEmpleado> HorariosEmpleados { get; set; } = new List<HorariosEmpleado>();
}
