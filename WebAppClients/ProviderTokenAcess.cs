using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAppClients
{
    public class ProviderTokenAcess : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = BaseUsers
                .Users()
                .FirstOrDefault(x => x.Name == context.UserName
                                && x.Password == context.Password);
            if(user == null)
            {
                context.SetError("invalid_grant", "Usuario não encontrado ou senha incorreta.");
                return;
            }

            var identityUser = new ClaimsIdentity(context.Options.AuthenticationType);

            foreach (var function in user.Functions)
            {
                identityUser.AddClaim(new Claim(ClaimTypes.Role, function));
            }

            context.Validated(identityUser);
        }
    }
}