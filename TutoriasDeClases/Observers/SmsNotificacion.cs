// Observers/SmsNotificacion.cs
using TutoriasDeClases.Interfaces;

namespace TutoriasDeClases.Observers
{
    public class SmsNotificacion : INotificacion
    {
        public void Enviar(string mensaje) => Console.WriteLine($"[SMS] {mensaje}");
    }
}