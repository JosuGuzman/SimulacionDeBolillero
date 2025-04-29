namespace Consola;
public class Sorteador : ISorteadorRandom
{
    private readonly Random _rnd = new Random();
    public int Next(int minValue, int maxValue) => _rnd.Next(minValue, maxValue);
}