using EmbeddedAuthorizationServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace EmbeddedAuthorizationServer.Controllers
{
    [Authorize]
    public class IdentityController : MyApiController
    {
        public IEnumerable<string> Get()
        {
            return new List<string>() { "username:" + UserName };
        }
    }

    //[Authorize]
    //public class IdentityController : ApiController
    //{
    //    public IEnumerable<ViewClaim> Get()
    //    {
    //        var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
    //        var userName = principal.Claims.Where(c => c.Type == "sub").Single().Value;
    //        return from c in principal.Claims
    //               select new ViewClaim
    //               {
    //                   Type = c.Type,
    //                   Value = c.Value
    //               };
    // [{"Type":"sub","Value":"bob"},{"Type":"role","Value":"user"}]
    //    }
    //}
}
