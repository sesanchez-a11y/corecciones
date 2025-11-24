// Observers/EmailNotificacion.cs
using TutoriasDeClases.Interfaces;

namespace TutoriasDeClases.Observers
{
    public class EmailNotificacion : INotificacion
    {
        public void Enviar(string mensaje) => Console.WriteLine($"[EMAIL] {mensaje}");
    }
}