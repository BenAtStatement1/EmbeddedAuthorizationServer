using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmbeddedAuthorizationServer.Provider
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // OAuth2 supports the notion of client authentication
            // this is not used here
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // validate user credentials (demo!)
            // user credentials should be stored securely (salted, iterated, hashed yada)
            // using a browser like Postman following oauth2 password granttype and
            // set grant_type=password, username and password
            if (context.UserName != null && context.UserName != context.Password)
            {
                context.Rejected();
                return;
            }

            // create identity
            var id = new ClaimsIdentity(context.Options.AuthenticationType);
            id.AddClaim(new Claim("sub", context.UserName));
            id.AddClaim(new Claim("role", "user"));

            context.Validated(id);
        }
    }
}