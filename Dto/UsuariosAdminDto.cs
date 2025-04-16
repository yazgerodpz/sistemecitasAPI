namespace sistemecitasAPI.Dto
{
    public class UsuariosAdminDto
    {
            public int IdUsuario { get; set; }
            public string Usuario { get; set; } = null!;
            public string ContrasenaHash { get; set; } = null!;
            public string? Rol { get; set; }
    }
}
