using System;
using System.Net.Http;
using Thinktecture.IdentityModel.Client;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TokenResponse tok = GetToken();
            Console.WriteLine("token:" + tok.AccessToken);
            CallService(tok.AccessToken);
        }

        private static TokenResponse GetToken()
        {
            var client = new OAuth2Client(
                new Uri("http://localhost/token"));

            return client.RequestResourceOwnerPasswordAsync("bob", "bob").Result;
        }

        private static void CallService(string token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);
            var response = client.GetStringAsync(new Uri("http://localhost/api/identity")).Result;

            Console.WriteLine(response);
            Console.ReadLine();
        }
    }
}