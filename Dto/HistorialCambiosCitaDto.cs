namespace sistemecitasAPI.Dto
{
    public class HistorialCambiosCitaDto
    {
        public int IdHistorial { get; set; }
        public int IdCita { get; set; }
        public DateTime? FechaCambio { get; set; }
        public string? DescripcionCambio { get; set; }
    }
}
