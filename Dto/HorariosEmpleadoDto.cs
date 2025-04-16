namespace sistemecitasAPI.Dto
{
    public class HorariosEmpleadoDto
    {
        public int IdHorario { get; set; }
        public int IdEmpleado { get; set; }
        public int DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
