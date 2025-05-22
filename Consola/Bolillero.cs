namespace Consola;

public class Bolillero : ICloneable
{
    private readonly ISorteadorRandom _sorteador; // Inyectado para permitir aleatoriedad flexible (pruebas o producción).
    private readonly List<int> _bolillasInside; // Bolillas actualmente dentro del bolillero.
    private readonly List<int> _bolillasOutside = new(); // Bolillas que han sido sacadas.

    public IReadOnlyList<int> BolillasInside => _bolillasInside; // Acceso de solo lectura a bolillas dentro.
    public IReadOnlyList<int> BolillasOutside => _bolillasOutside; // Acceso de solo lectura a bolillas fuera.

    // Constructor que recibe las bolillas iniciales y un generador de aleatoriedad.
    public Bolillero(IEnumerable<int> bolillasIniciales, ISorteadorRandom sorteador)
    {
        _bolillasInside = new List<int>(bolillasIniciales);
        _sorteador = sorteador;
    }

    // Saca una bolilla al azar del bolillero y la pasa a la lista de bolillas fuera.
    public int SacarBolilla()
    {
        if (_bolillasInside.Count == 0)
            throw new InvalidOperationException("No hay más bolillas para sacar.");

        int idx = _sorteador.Next(0, _bolillasInside.Count); // Índice aleatorio.
        int valor = _bolillasInside[idx]; // Valor extraído.

        _bolillasInside.RemoveAt(idx);
        _bolillasOutside.Add(valor);

        return valor;
    }

    // Reintegra todas las bolillas que estaban fuera de vuelta al bolillero.
    public void ReIngresar()
    {
        _bolillasInside.AddRange(_bolillasOutside);
        _bolillasOutside.Clear();
    }

    // Simula una jugada: intenta sacar bolillas en el orden exacto de la lista proporcionada.
    // Si alguna bolilla no coincide, reintegra y devuelve false. Si todas coinciden, devuelve true.
    public bool Jugar(IList<int> jugada)
    {
        foreach (var objetivo in jugada)
        {
            int sacada = SacarBolilla();
            if (sacada != objetivo)
            {
                ReIngresar();
                return false;
            }
        }

        ReIngresar();
        return true;
    }

    // Repite la jugada una cierta cantidad de veces y devuelve cuántas veces fue exitosa.
    public long JugarNVeces(IList<int> jugada, long veces)
    {
        long aciertos = 0;

        for (long i = 0; i < veces; i++)
            if (Jugar(jugada))
                aciertos++;

        return aciertos;
    }

    public object Clone()
    {
        var copiaInside = new List<int>(_bolillasInside);
        var copia = new Bolillero(copiaInside, _sorteador);
        foreach (var bolilla in _bolillasOutside)
            copia._bolillasOutside.Add(bolilla);

        return copia;
    }
}