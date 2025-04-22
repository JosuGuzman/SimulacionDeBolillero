using Consola;
namespace TestsBolillero
{
    public class BolilleroTests
    {
        private readonly Bolillero _bolillero;

        public BolilleroTests()
        {
            _bolillero = new Bolillero(
                Enumerable.Range(0, 10), new PruebaSorteador() );
        }

        [Fact]
        public void SacarBolilla_DeberiaDevolverCero_YActualizarListas()
        {
            int sacada = _bolillero.SacarBolilla();
            Assert.Equal(0, sacada);
            Assert.Equal(9, _bolillero.BolillasInside.Count);
            Assert.Single(_bolillero.BolillasOutside);
        }

        [Fact]
        public void ReIngresar_DeberiaRestaurarTodasLasBolillas()
        {
            _bolillero.SacarBolilla();
            _bolillero.ReIngresar();
            Assert.Equal(10, _bolillero.BolillasInside.Count);
            Assert.Empty(_bolillero.BolillasOutside);
        }

        [Fact]
        public void JugarGana_CuandoLaJugadaEs0_1_2_3()
        {
            var jugada = new List<int> { 0, 1, 2, 3 };
            Assert.True(_bolillero.Jugar(jugada));
        }

        [Fact]
        public void JugarPierde_CuandoLaJugadaEs4_2_1()
        {
            var jugada = new List<int> { 4, 2, 1 };
            Assert.False(_bolillero.Jugar(jugada));
        }

        [Fact]
        public void GanarNVeces_ConJugada01_Y1Vez_DeberiaSer1()
        {
            var jugada = new List<int> { 0, 1 };
            long resultado = _bolillero.JugarNVeces(jugada, 1);
            Assert.Equal(1, resultado);
        }
    }
}