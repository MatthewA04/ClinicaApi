namespace ClinicaApi.DTOs
{
    public class CitaRespuestaDto
    {
        public int Id { get; set; }
        public string Paciente { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Medico { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string MotivoConsulta { get; set; } = string.Empty;
    }
}