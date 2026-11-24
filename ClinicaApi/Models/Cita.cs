namespace ClinicaApi.Models
{
    public class Cita
    {
        public int Id { get; set; }
        public Paciente Paciente { get; set; } = new();
        public Medico Medico { get; set; } = new();
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string MotivoConsulta { get; set; } = string.Empty;
    }
}