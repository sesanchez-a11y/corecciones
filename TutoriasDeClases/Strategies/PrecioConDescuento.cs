// Strategies/PrecioConDescuento.cs
using TutoriasDeClases.Interfaces;

namespace TutoriasDeClases.Strategies
{
    public class PrecioConDescuento : IPrecioStrategy
    {
        public double Calcular(double precioBase) => precioBase * 0.9;
    }
}