// Strategies/PrecioConImpuesto.cs
using TutoriasDeClases.Interfaces;

namespace TutoriasDeClases.Strategies
{
    public class PrecioConImpuesto : IPrecioStrategy
    {
        public double Calcular(double precioBase) => precioBase * 1.12;
    }
}