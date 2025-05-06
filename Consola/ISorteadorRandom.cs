// Archivo: ISorteadorRandom.cs
// Interfaz que define un contrato para la generación de números aleatorios.
// Se utiliza para desacoplar la lógica de aleatoriedad del resto del sistema,
// permitiendo sustituir fácilmente el comportamiento en pruebas o diferentes implementaciones.
namespace Consola;

public interface ISorteadorRandom
{
    int Next(int minValue, int maxValue); // Devuelve un número aleatorio entre minValue (incluido) y maxValue (excluido).
}