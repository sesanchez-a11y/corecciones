using TutoriasDeClases.Interfaces;

namespace TutoriasDeClases.Strategies
{
    public class PrecioBase : IPrecioStrategy
    {
        public double Calcular(double precioBase) => precioBase;
    }
}