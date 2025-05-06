// Archivo: PruebaSorteador.cs
// Implementación de ISorteadorRandom para pruebas. Siempre devuelve el mismo valor (0).
// Útil para probar resultados deterministas en lugar de usar aleatoriedad.
namespace Consola;

public class PruebaSorteador : ISorteadorRandom
{
    public int Next(int minValue, int maxValue) => 0; // Siempre retorna 0 sin importar el rango.
}