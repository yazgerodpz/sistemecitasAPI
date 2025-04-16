namespace sistemecitasAPI.Dto
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Apellidos { get; set; }
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
