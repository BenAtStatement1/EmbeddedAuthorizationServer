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
            if (tok != null)
            {
                Console.WriteLine("token endpoint:" + tok.AccessToken);
                var response=CallService(tok.AccessToken);
                Console.WriteLine("identity endpoint:" + response);
                Console.ReadLine();
            }
        }

        private static TokenResponse GetToken()
        {
            try
            {
                var client = new OAuth2Client(
                new Uri("http://localhost/token"));

                return client.RequestResourceOwnerPasswordAsync("bob", "bob").Result;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return null;
        }

        private static string CallService(string token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);
            var response = client.GetStringAsync(new Uri("http://localhost/api/identity")).Result;

            return response;
        }
    }
}