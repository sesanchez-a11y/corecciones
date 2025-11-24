namespace TutoriasDeClases.Modelos
{
    public class Tutor : Usuario
    {
        public int Edad { get; set; }
        public string Especializacion { get; set; } = string.Empty;
        // Puedes agregar más campos específicos del tutor si necesitas
    }
}