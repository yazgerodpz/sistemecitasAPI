using System;
using System.Collections.Generic;

namespace sistemecitasAPI.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int DuracionMinutos { get; set; }

    public decimal Precio { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
