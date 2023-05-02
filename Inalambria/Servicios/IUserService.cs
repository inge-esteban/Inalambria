namespace Inalambria.Servicios
{
    public interface IUserService
    {
        /// <summary>
        /// Determina si el usuario coincide con la contraseña
        /// </summary>
        /// <param name="email">correo electronico de la persona en que inicio seccion</param>
        /// <param name="contraseña">contraseña de la persona</param>
        /// <returns>retorna true si el email y contraseña de la persona coinciden</returns>
        public bool IsUser(string email,string contraseña);
    }
}
