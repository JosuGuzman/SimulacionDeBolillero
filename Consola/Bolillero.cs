namespace Consola;

public class Bolillero
{
    private readonly ISorteadorRandom _sorteador;
    private readonly List<int> _bolillasInside; // Lista que representa las bolillas que estan dentro del Bolillero.
    private readonly List<int> _bolillasOutside = new(); // Lista que representa las bolillas que ya fueron sacadas del Bolillero.

    public IReadOnlyList<int> BolillasInside => _bolillasInside; //Permite visualizar las bolillas dentro del Bolillero sin modificar directamente.
    public IReadOnlyList<int> BolillasOutside => _bolillasOutside; //Permite visualizar las bolillas fuera de Bolillero sin modificar directamente.

    // Constructor de la clase 
    public Bolillero(IEnumerable<int> bolillasIniciales, ISorteadorRandom sorteador)
    {
        _bolillasInside = new List <int> (bolillasIniciales);
        _sorteador = sorteador;
    }

    // Funcion SacarBolillero
    public int SacarBolilla()
    {
        // Verificar que contenga bolillas el Bolillero
        if (_bolillasInside.Count == 0)
            throw new InvalidOperationException("No hay más bolillas para sacar.");
        
        // 
        int idx = _sorteador.Next(0, _bolillasInside.Count);
        
        // inplementar el valor de la bolilla que salio
        int valor = _bolillasInside[idx];
        
        // remover la bolilla de la lista de valores dentro del Bolillero
        _bolillasInside.RemoveAt(idx);
        
        //ingresar el valor de bolilla a la lista de las que salieron
        _bolillasOutside.Add(valor);
        
        return valor; // Devolver el valor de la bolilla que salio
    }

    // Funcion ReIngresar 
    public void ReIngresar()
    {
        //
        _bolillasInside.AddRange(_bolillasOutside);
        
        //
        _bolillasOutside.Clear();
    }

    // Función Jugar
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

    // Funcion JugarNVeces
    public long JugarNVeces(IList<int> jugada, long veces)
    {
        long aciertos = 0;
        
        for (long i = 0; i < veces; i++)
            
            if (Jugar(jugada))    
                aciertos++;
        
        return aciertos;
    }
}