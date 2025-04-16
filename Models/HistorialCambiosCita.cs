using System;
using System.Collections.Generic;

namespace sistemecitasAPI.Models;

public partial class HistorialCambiosCita
{
    public int IdHistorial { get; set; }

    public int IdCita { get; set; }

    public DateTime? FechaCambio { get; set; }

    public string? DescripcionCambio { get; set; }

    public virtual Cita IdCitaNavigation { get; set; } = null!;
}
