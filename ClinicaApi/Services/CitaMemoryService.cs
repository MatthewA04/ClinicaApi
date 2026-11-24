using ClinicaApi.DTOs;
using ClinicaApi.Models;

namespace ClinicaApi.Services
{
    public class CitaMemoryService : ICitaService
    {
        private readonly List<Cita> _citas = new();
        private int _nextId = 1;

        public CitaMemoryService()
        {
            // Insertamos una cita inicial de prueba (semilla)
            Crear(new CitaCreacionDto
            {
                NombrePaciente = "Juan Pérez",
                DniPaciente = "12345678",
                NombreMedico = "Dr. Carlos Mendoza",
                EspecialidadMedico = "Cardiología",
                FechaHora = DateTime.Now.AddDays(2),
                MotivoConsulta = "Chequeo de rutina"
            });
        }

        public IEnumerable<CitaRespuestaDto> ObtenerTodas()
        {
            return _citas.Select(MapToDto);
        }

        public CitaRespuestaDto ObtenerPorId(int id)
        {
            var cita = _citas.FirstOrDefault(c => c.Id == id);
            if (cita == null)
            {
                throw new KeyNotFoundException($"No se encontró la cita con ID {id}.");
            }
            return MapToDto(cita);
        }

        public CitaRespuestaDto Crear(CitaCreacionDto dto)
        {
            var cita = new Cita
            {
                Id = _nextId++,
                Paciente = new Paciente
                {
                    Id = _nextId,
                    Nombre = dto.NombrePaciente,
                    Dni = dto.DniPaciente
                },
                Medico = new Medico
                {
                    Id = _nextId,
                    Nombre = dto.NombreMedico,
                    Especialidad = dto.EspecialidadMedico
                },
                FechaHora = dto.FechaHora,
                Estado = "Pendiente",
                MotivoConsulta = dto.MotivoConsulta
            };

            _citas.Add(cita);
            return MapToDto(cita);
        }

        public CitaRespuestaDto Actualizar(int id, CitaCreacionDto dto)
        {
            var cita = _citas.FirstOrDefault(c => c.Id == id);
            if (cita == null)
            {
                throw new KeyNotFoundException($"No se encontró la cita con ID {id}.");
            }

            cita.Paciente.Nombre = dto.NombrePaciente;
            cita.Paciente.Dni = dto.DniPaciente;
            cita.Medico.Nombre = dto.NombreMedico;
            cita.Medico.Especialidad = dto.EspecialidadMedico;
            cita.FechaHora = dto.FechaHora;
            cita.MotivoConsulta = dto.MotivoConsulta;

            return MapToDto(cita);
        }

        public bool CancelarOCambiarEstado(int id, string nuevoEstado)
        {
            var cita = _citas.FirstOrDefault(c => c.Id == id);
            if (cita == null)
            {
                throw new KeyNotFoundException($"No se encontró la cita con ID {id}.");
            }

            cita.Estado = nuevoEstado;
            return true;
        }

        public bool Eliminar(int id)
        {
            var cita = _citas.FirstOrDefault(c => c.Id == id);
            if (cita == null) return false;

            return _citas.Remove(cita);
        }

        private static CitaRespuestaDto MapToDto(Cita cita) => new()
        {
            Id = cita.Id,
            Paciente = cita.Paciente.Nombre,
            Dni = cita.Paciente.Dni,
            Medico = cita.Medico.Nombre,
            Especialidad = cita.Medico.Especialidad,
            FechaHora = cita.FechaHora,
            Estado = cita.Estado,
            MotivoConsulta = cita.MotivoConsulta
        };
    }
}