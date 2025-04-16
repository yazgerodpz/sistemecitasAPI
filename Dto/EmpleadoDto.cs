namespace sistemecitasAPI.Dto
{
    public class EmpleadoDto
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Especialidad { get; set; }
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public bool? Activo { get; set; }
    }
}
