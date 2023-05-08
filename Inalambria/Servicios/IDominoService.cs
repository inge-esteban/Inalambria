using Inalambria.Modelos;

namespace Inalambria.Servicios
{
    public interface IDominoService
    {
        public List<ModeloFicha> Get();
        public string ComprobarQueSePuedeRealizarCadena(List<ModeloFicha> lista);
        public List<ModeloFicha> ExaminarLista(List<ModeloFicha> lista);
    }
}
