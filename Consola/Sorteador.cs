// Archivo: Sorteador.cs
// Implementación real de ISorteadorRandom usando la clase Random de .NET.
// Esta clase genera números pseudoaleatorios verdaderos y se usa en la simulación real.
namespace Consola;

public class Sorteador : ISorteadorRandom
{
    private readonly Random _rnd = new Random(); // Generador aleatorio del sistema.

    public int Next(int minValue, int maxValue) => _rnd.Next(minValue, maxValue); // Devuelve un número aleatorio entre minValue (incluido) y maxValue (excluido).
}