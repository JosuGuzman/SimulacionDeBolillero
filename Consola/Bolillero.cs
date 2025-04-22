namespace Consola;

public class Bolillero : IClonable <Bolillero>
{
    private readonly ISorteadorRandom _sorteador;
    private readonly List<int> _bolillasInside;
    private readonly List<int> _bolillasOutside = new();

    public IReadOnlyList<int> BolillasInside => _bolillasInside;
    public IReadOnlyList<int> BolillasOutside => _bolillasOutside;

    public Bolillero(IEnumerable<int> bolillasIniciales, ISorteadorRandom sorteador)
    {
        _bolillasInside = new List<int>(bolillasIniciales);
        _sorteador = sorteador;
    }

    private Bolillero(Bolillero original)
    {
        _bolillasInside = new List<int>(original._bolillasInside);
        _bolillasOutside = new List<int>(original._bolillasOutside);
        _sorteador = new Sorteador();        
    }

    public Bolillero Clonar()
    {
        return new Bolillero(this);
    }

    public int SacarBolilla()
    {
        if (_bolillasInside.Count == 0)
            throw new InvalidOperationException("No hay más bolillas para sacar.");
        
        int idx = _sorteador.Next(0, _bolillasInside.Count);
        
        int valor = _bolillasInside[idx];
        
        _bolillasInside.RemoveAt(idx);
        
        _bolillasOutside.Add(valor);
        return valor;
    }

    public void ReIngresar()
    {
        _bolillasInside.AddRange(_bolillasOutside);
        _bolillasOutside.Clear();
    }

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

    public long JugarNVeces(IList<int> jugada, long veces)
    {
        long aciertos = 0;
        
        for (long i = 0; i < veces; i++)
            
            if (Jugar(jugada))    
                aciertos++;
        
        return aciertos;
    }
}