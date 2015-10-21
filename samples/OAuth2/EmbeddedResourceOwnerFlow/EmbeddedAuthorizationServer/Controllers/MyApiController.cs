using EmbeddedAuthorizationServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace EmbeddedAuthorizationServer.Controllers
{
    public class MyApiController : ApiController
    {
        public ClaimsPrincipal Principal;
        public string UserName="";
        public MyApiController()
        {
            try
            {
                if (this.RequestContext.Principal != null)
                {
                    Principal = this.RequestContext.Principal as ClaimsPrincipal;
                    UserName = Principal.Claims.Where(c => c.Type == "sub").Single().Value;
                };
            }
            catch (System.Exception ex)
            {
                string s = ex.Message;
            }
        }

    }
}
