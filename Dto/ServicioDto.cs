namespace sistemecitasAPI.Dto
{
    public class ServicioDto
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int DuracionMinutos { get; set; }
        public decimal Precio { get; set; }
    }
}
