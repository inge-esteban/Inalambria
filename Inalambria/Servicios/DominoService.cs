using Inalambria.Modelos;

namespace Inalambria.Servicios
{
    public class DominoService:IDominoService
    {
        public List<ModeloFicha> Get()
        {
            return new List<ModeloFicha>();
        }
        /// <summary>
        /// Comprueba que las fichas de la lista tengan un par
        /// </summary>
        /// <param name="lista">lista de fichas que se desea examinar</param>
        /// <returns>un string en caso de que no se pueda resolver la lista</returns>
        public string ComprobarQueSePuedeRealizarCadena(List<ModeloFicha> lista)
        {
            //comprobar que todos los numeros tengan un par
            foreach (ModeloFicha modeloFicha in lista)
            {
                if (lista.Where(x => x.posicion1 == modeloFicha.posicion1 || x.posicion1 == modeloFicha.posicion2).Count() < 2)
                    return "La ficha " + modeloFicha.posicion1 + " | " + modeloFicha.posicion2 + " No tiene otro par";
                if (lista.Where(x => x.posicion2 == modeloFicha.posicion2 || x.posicion1 == modeloFicha.posicion2).Count() < 2)
                    return "La ficha " + modeloFicha.posicion1 + " | " + modeloFicha.posicion2 + " No tiene otro par";
                if (modeloFicha.posicion1 == modeloFicha.posicion2)
                    if (lista.Where(x => x.posicion1 == modeloFicha.posicion1 || x.posicion2 == modeloFicha.posicion1).Count() < 3)
                        return "La ficha " + modeloFicha.posicion1 + " | " + modeloFicha.posicion2 + " No tiene otro par";
            }
            return "";
        }
        /// <summary>
        /// Examina y resuelve la lista previamente evaluada
        /// </summary>
        /// <param name="lista">lista que se desea evaluar si se puede ordenar</param>
        /// <returns>retorna la lista que se mando por parametros de forma ordenada</returns>
        public List<ModeloFicha> ExaminarLista(List<ModeloFicha> lista)
        {
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
                }
            }

            return Result;

        }
    }
}
