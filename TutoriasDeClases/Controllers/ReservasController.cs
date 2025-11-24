using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TutoriasDeClases.Modelos;
using TutoriasDeClases.Factories;
using TutoriasDeClases.Strategies;
using TutoriasDeClases.Observers;
using TutoriasDeClases.Interfaces;

namespace TutoriasDeClases.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly IMongoCollection<Servicio> _servicios;
        private readonly IMongoCollection<Reserva> _reservas;

        public ReservasController(IMongoCollection<Servicio> servicios, IMongoCollection<Reserva> reservas)
        {
            _servicios = servicios;
            _reservas = reservas;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearReserva([FromBody] CrearReservaRequest request)
        {
            // Buscar servicio
            var servicio = await _servicios.Find(s => s.Id == request.ServicioId).FirstOrDefaultAsync();
            if (servicio == null)
                return BadRequest("Servicio no encontrado");

            // Aplicar estrategia
            IPrecioStrategy estrategia = new PrecioConDescuento();
            double total = estrategia.Calcular(servicio.PrecioBase);

            // Crear reserva
            var reserva = ReservaFactory.CrearReservaIndividual(servicio, request.AlumnoId, total);

            // Notificar
            reserva.AgregarObservador(new EmailNotificacion());
            reserva.AgregarObservador(new SmsNotificacion());
            reserva.Confirmar();

            // Guardar
            await _reservas.InsertOneAsync(reserva);

            return Ok(new { Mensaje = "Reserva creada", Id = reserva.Id, Total = total });
        }
    }

    public class CrearReservaRequest
    {
        public string ServicioId { get; set; } = string.Empty;
        public string AlumnoId { get; set; } = string.Empty;
    }
}