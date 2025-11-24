using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TutoriasDeClases.Modelos
{
    public class Servicio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [BsonElement("precioBase")]
        public double PrecioBase { get; set; }
    }
}