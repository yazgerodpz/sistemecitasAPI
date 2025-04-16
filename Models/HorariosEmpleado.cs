using System;
using System.Collections.Generic;

namespace sistemecitasAPI.Models;

public partial class HorariosEmpleado
{
    public int IdHorario { get; set; }

    public int IdEmpleado { get; set; }

    public int DiaSemana { get; set; }

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFin { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
}
