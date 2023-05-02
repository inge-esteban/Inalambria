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
            if (lista.Count > 6 || lista.Count < 2)
            {
                ModelState.AddModelError("ValidacionTamañoLista", "El arreglo solo permite maximo 6 y minimo 2 fichas.");
                return BadRequest(ModelState);
            }
            List<ModeloFicha> Result = new List<ModeloFicha>();
            ModeloFicha ficha;
            ficha = lista[0];
            Result.Add(ficha);
            lista.Remove(ficha);

            while (lista.Count > 0)
            {
                foreach (ModeloFicha item in lista)
                {
                    if (item.posicion1 == Result[0].posicion1)
                    {
                        Result = Result.Prepend(new ModeloFicha
                        {
                            posicion1 = item.posicion2,
                            posicion2 = item.posicion1
                        }).ToList();
                        lista.Remove(item);
                        break;
                    }
                    if (item.posicion2 == Result[0].posicion1)
                    {
                        Result = Result.Prepend(item).ToList();
                        lista.Remove(item);
                        break;
                    }
                    else if (item.posicion1 == Result.Last().posicion2)
                    {
                        Result.Add(item);
                        lista.Remove(item);
                        break;
                    }
                    if (item.posicion2 == Result.Last().posicion2)
                    {
                        Result.Add(new ModeloFicha
                        {
                            posicion1 = item.posicion2,
                            posicion2 = item.posicion1
                        });
                        lista.Remove(item);
                        break;
                    }

                    //if (item == lista.Last())
                    //{
                    //    ModelState.AddModelError("ValidacionNoSePuede", "No se pudo crear la cadena con los parametros enviados.");
                    //    return BadRequest(ModelState);
                    //}
                }
            }


            if (Result[0].posicion1 == Result.Last().posicion2)
                return Ok(Result);
            else
            {
                ModelState.AddModelError("ValidacionPuntas", "Los números primero y último no son los mismos.");
                return BadRequest(ModelState);
            }

        }
    }
}
