namespace Consola;

public class Simulacion
{
    public long SimularSinHilos(Bolillero bolillero, IList<int> jugada, long cantidadSimulaciones)
    {
        return bolillero.JugarNVeces(jugada, cantidadSimulaciones);
    }

    public async Task<long> SimularConHilosAsync(
        Bolillero bolillero,
        IList<int> jugada,
        long cantidadSimulaciones,
        int cantidadHilos)
    {
        long basePorHilo = cantidadSimulaciones / cantidadHilos;
        long resto = cantidadSimulaciones % cantidadHilos;

        var tareas = new List<Task<long>>();

        for (int i = 0; i < cantidadHilos; i++)
        {
            long simulaciones = basePorHilo + (i < resto ? 1 : 0);
            Bolillero clon = bolillero.Clonar();

            tareas.Add(Task.Run(() => clon.JugarNVeces(jugada, simulaciones)));
        }

        long[] resultados = await Task.WhenAll(tareas);
        return resultados.Sum();
    }
}