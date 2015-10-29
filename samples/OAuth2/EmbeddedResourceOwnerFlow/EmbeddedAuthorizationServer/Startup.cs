using EmbeddedAuthorizationServer.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

[assembly: OwinStartup(typeof(EmbeddedAuthorizationServer.Startup))]

namespace EmbeddedAuthorizationServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // token generation
            var serverOptions = new OAuthAuthorizationServerOptions
            {
                // for demo purposes
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(8),
                Provider = new SimpleAuthorizationServerProvider(),
                AuthenticationMode = AuthenticationMode.Active,
                AuthenticationType = "Bearer"
            };
#if Debug
            System.Diagnostics.Debugger.Launch();
#endif
            app.UseOAuthAuthorizationServer(serverOptions); // Adds OAuth2 Authorization Server capabilities to an OWIN web application.
            /*  
            This middleware performs the request processing for the Authorize and Token endpoints defined by the OAuth2 specification. 
            See also http://tools.ietf.org/html/rfc6749
            */
            // token consumption
            var bearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(bearerOptions); // Adds Bearer token processing to an OWIN application pipeline. 
            /* 
            This middleware understands appropriately formatted and 
            secured tokens which appear in the request header. If the Options.AuthenticationMode is Active, the claims within the 
            bearer token are added to the current request's IPrincipal User. 
            See also http://tools.ietf.org/html/rfc6749 
            */
            app.UseWebApi(WebApiConfig.Register()); // add WebAPI endpoint processing to OWIN pipeline
        }
    }
}

/* 
? serverOptions
{Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerOptions}
    AccessTokenExpireTimeSpan: {08:00:00}
    AccessTokenFormat: null
    AccessTokenProvider: null
    AllowInsecureHttp: true
    ApplicationCanDisplayErrors: false
    AuthenticationMode: Active
    AuthenticationType: "Bearer"
    AuthorizationCodeExpireTimeSpan: {00:05:00}
    AuthorizationCodeFormat: null
    AuthorizationCodeProvider: null
    AuthorizeEndpointPath: {}
    Description: {Microsoft.Owin.Security.AuthenticationDescription}
    Provider: {EmbeddedAuthorizationServer.Provider.SimpleAuthorizationServerProvider}
    RefreshTokenFormat: null
    RefreshTokenProvider: null
    SystemClock: {Microsoft.Owin.Infrastructure.SystemClock}
    TokenEndpointPath: {/token}
? bearerOptions
{Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationOptions}
    AccessTokenFormat: null
    AccessTokenProvider: null
    AuthenticationMode: Active
    AuthenticationType: "Bearer"
    Description: {Microsoft.Owin.Security.AuthenticationDescription}
    Provider: null
    Realm: null
    SystemClock: {Microsoft.Owin.Infrastructure.SystemClock}
    
  */
