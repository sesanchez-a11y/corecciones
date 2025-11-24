using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TutoriasDeClases.Interfaces;
using System.Collections.Generic;

namespace TutoriasDeClases.Modelos
{
    public class Reserva
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("servicio")]
        public Servicio Servicio { get; set; } = new();

        [BsonElement("alumnoId")]
        public string AlumnoId { get; set; } = string.Empty;

        [BsonElement("total")]
        public double Total { get; set; }

        private List<INotificacion> _observadores = new();

        public void AgregarObservador(INotificacion obs) => _observadores.Add(obs);

        public void Notificar(string mensaje)
        {
            foreach (var obs in _observadores)
                obs.Enviar(mensaje);
        }

        public void Confirmar()
        {
            Notificar("Tu reserva ha sido confirmada.");
        }
    }
}