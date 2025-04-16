using System;
using System.Collections.Generic;

namespace sistemecitasAPI.Models;

public partial class UsuariosAdmin
{
    public int IdUsuario { get; set; }

    public string Usuario { get; set; } = null!;

    public string ContrasenaHash { get; set; } = null!;

    public string? Rol { get; set; }
}
