using ClinicaApi.DTOs;
using ClinicaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitasController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        // GET: api/citas
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_citaService.ObtenerTodas());
        }

        // GET: api/citas/1
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var cita = _citaService.ObtenerPorId(id);
            return Ok(cita);
        }

        // POST: api/citas
        [HttpPost]
        public IActionResult Create([FromBody] CitaCreacionDto dto)
        {
            var nuevaCita = _citaService.Crear(dto);
            return CreatedAtAction(nameof(GetById), new { id = nuevaCita.Id }, nuevaCita);
        }

        // PUT: api/citas/1
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] CitaCreacionDto dto)
        {
            var actualizada = _citaService.Actualizar(id, dto);
            return Ok(actualizada);
        }

        // PATCH: api/citas/1/estado
        [HttpPatch("{id:int}/estado")]
        public IActionResult PatchEstado(int id, [FromBody] string nuevoEstado)
        {
            _citaService.CancelarOCambiarEstado(id, nuevoEstado);
            return Ok(new { mensaje = $"El estado de la cita con ID {id} cambió a '{nuevoEstado}' correctamente." });
        }

        // DELETE: api/citas/1
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var eliminada = _citaService.Eliminar(id);
            if (!eliminada)
            {
                throw new KeyNotFoundException($"No existe una cita con ID {id} para eliminar.");
            }

            return NoContent();
        }
    }
}