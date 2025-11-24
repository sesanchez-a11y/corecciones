namespace TutoriasDeClases.Modelos
{
    public class Estudiante : Usuario
    {
        public int Edad { get; set; }
        public string Especializacion { get; set; } = string.Empty;
    }
}