using Inalambria.Modelos;

namespace Inalambria.Servicios
{
    public class UserService : IUserService
    {
        List<ModeloUser> listaUsuarios = new List<ModeloUser>()
        {
            new ModeloUser { email="esteban@gmail.com",contraseña="123456"},
            new ModeloUser { email="luz@inalambria.com", contraseña="abcdef"}
        };

        /// <summary>
        /// Determina si el usuario coincide con la contraseña
        /// </summary>
        /// <param name="email">correo electronico de la persona en que inicio seccion</param>
        /// <param name="contraseña">contraseña de la persona</param>
        /// <returns>retorna true si el email y contraseña de la persona coinciden</returns>
        public bool IsUser(string email, string contraseña)
        {
            return listaUsuarios.Where(x => x.email == email && x.contraseña ==contraseña).Any();
        }
    }
}
