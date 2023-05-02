using Inalambria.Servicios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Inalambria.Seguridad
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;

        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService userService) : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("El header no contiene una la Authorization ");

            bool result = false;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credencialByte = Convert.FromBase64String(authHeader.Parameter);
                var credenciales = Encoding.UTF8.GetString(credencialByte).Split(new[] { ':' }, 2);
                var email = credenciales[0];
                var contraseña = credenciales[1];
                result = _userService.IsUser(email, contraseña);

            }
            catch
            {
                return AuthenticateResult.Fail("A ocurrido un error");
            }

            if (!result)
                return AuthenticateResult.Fail("Usuario o contraseña no valida");

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,"id"),
                new Claim(ClaimTypes.Name,"users")
                            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);


        }


    }
}
