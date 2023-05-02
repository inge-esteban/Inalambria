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


        public bool IsUser(string email, string contraseña)
        {
            return listaUsuarios.Where(x => x.email == email && x.contraseña ==contraseña).Any();
        }
    }
}
