namespace Consola;

public class Simulacion
{
    public long SimularSinHilos(Bolillero bolillero, IList<int> jugada, long cantidad)
    {
        return bolillero.JugarNVeces(jugada, cantidad);
    }

    public long SimularConHilos(Bolillero bolillero, IList<int> jugada, long cantidad, int hilos)
    {
        long total = 0;
        var resultados = new long[hilos];
        var tareas = new List<Thread>();
        long porHilo = cantidad / hilos;

        for (int i = 0; i < hilos; i++)
        {
            int idx = i;
            tareas.Add(new Thread(() =>
            {
                var clon = (Bolillero)bolillero.Clone();
                resultados[idx] = clon.JugarNVeces(jugada, porHilo);
            }));
        }

        tareas.ForEach(t => t.Start());
        tareas.ForEach(t => t.Join());

        foreach (var r in resultados) total += r;
        return total;
    }

    public async Task<long> SimularConHilosAsync(Bolillero bolillero, IList<int> jugada, long cantidad, int hilos)
    {
        long porHilo = cantidad / hilos;
        var tareas = new List<Task<long>>();

        for (int i = 0; i < hilos; i++)
        {
            tareas.Add(Task.Run(() =>
            {
                var clon = (Bolillero)bolillero.Clone();
                return clon.JugarNVeces(jugada, porHilo);
            }));
        }

        long[] resultados = await Task.WhenAll(tareas);
        return resultados.Sum();
    }

}