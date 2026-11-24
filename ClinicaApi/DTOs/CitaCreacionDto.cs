using System.ComponentModel.DataAnnotations;

namespace ClinicaApi.DTOs
{
    public class CitaCreacionDto
    {
        [Required(ErrorMessage = "El nombre del paciente es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string NombrePaciente { get; set; } = string.Empty;

        [Required(ErrorMessage = "El DNI del paciente es obligatorio.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe tener exactamente 8 dígitos.")]
        public string DniPaciente { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del médico es obligatorio.")]
        public string NombreMedico { get; set; } = string.Empty;

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        public string EspecialidadMedico { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de la cita es obligatoria.")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "El motivo de la consulta es obligatorio.")]
        [StringLength(250, ErrorMessage = "El motivo no puede exceder los 250 caracteres.")]
        public string MotivoConsulta { get; set; } = string.Empty;
    }
}