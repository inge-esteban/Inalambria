using Inalambria.Modelos;
using Inalambria.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inalambria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //    [Authorize]
    public class DominoController : ControllerBase
    {
        private readonly IDominoService _dominoService;
        public DominoController(IDominoService dominoService)
        {
            _dominoService = dominoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ModeloFicha>> GetModeloFichas()
        {
            return _dominoService.Get();
        }


        [HttpPost]
        public ActionResult<IEnumerable<ModeloFicha>> PostModeloFichas(List<ModeloFicha> lista)
        {
            //Validar si lista tiene un maximo y minimo
            if (lista.Count > 6 || lista.Count < 2)
            {
                ModelState.AddModelError("ValidacionTamañoLista", "El arreglo solo permite maximo 6 y minimo 2 fichas.");
                return BadRequest(ModelState);
            }

            ///Comprueba si se puede realizar la cadena de acuerdo a las fichas pares
            string validacion = _dominoService.ComprobarQueSePuedeRealizarCadena(lista);
            if (validacion.Length > 1)
            {
                ModelState.AddModelError("ValidacionPoderRealizar", validacion);
                return BadRequest(ModelState);
            }

            ///Comprueba que las fichas si tengan los lados iguales
            List<ModeloFicha> Result = _dominoService.ExaminarLista(lista);
            if (Result[0].posicion1 == Result.Last().posicion2)
                return Ok(Result);
            else if (Result[0].posicion1 != Result.Last().posicion2)
            {
                ModelState.AddModelError("ValidacionPuntas", "Los números primero y último no son los mismos.");
                return BadRequest(ModelState);
            }
            return BadRequest();
        }
    }
}
