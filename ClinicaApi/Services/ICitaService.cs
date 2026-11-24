using ClinicaApi.DTOs;

namespace ClinicaApi.Services
{
    public interface ICitaService
    {
        IEnumerable<CitaRespuestaDto> ObtenerTodas();
        CitaRespuestaDto ObtenerPorId(int id);
        CitaRespuestaDto Crear(CitaCreacionDto dto);
        CitaRespuestaDto Actualizar(int id, CitaCreacionDto dto);
        bool CancelarOCambiarEstado(int id, string nuevoEstado);
        bool Eliminar(int id);
    }
}