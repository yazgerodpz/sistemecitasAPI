namespace sistemecitasAPI.Dto
{
    public class CitaDto
    {
        public int IdCita { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public int IdServicio { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public string? Estado { get; set; }
        public string? Notas { get; set; }
    }
}
