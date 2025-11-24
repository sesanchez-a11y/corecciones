using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TutoriasDeClases.Interfaces;

namespace TutoriasDeClases.Modelos
{
    public class Pago
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("monto")]
        public double Monto { get; set; }

        // Aplica SOLID: depende de interfaz, no de implementación
        public IPago Procesador { get; set; } = null!;
    }
}