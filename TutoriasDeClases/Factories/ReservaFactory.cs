using TutoriasDeClases.Modelos;

namespace TutoriasDeClases.Factories
{
    public static class ReservaFactory
    {
        public static Reserva CrearReservaIndividual(Servicio servicio, string alumnoId, double total)
        {
            return new Reserva
            {
                Servicio = servicio,
                AlumnoId = alumnoId,
                Total = total
            };
        }

        // En Fase 2: CrearReservaGrupal(), CrearReservaOnline(), etc.
    }
}