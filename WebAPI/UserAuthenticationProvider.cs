using EmbedIO;
using EmbedIO.BearerToken;
using EmbedIO.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFWebServer.WebAPI
{
    class UserAuthenticationProvider : IAuthorizationServerProvider
    {
        public long GetExpirationDate() => DateTime.UtcNow.AddHours(12).Ticks;

        public async Task ValidateClientAuthentication(ValidateClientAuthenticationContext context)
        {
            var data = await context.HttpContext.GetRequestFormDataAsync().ConfigureAwait(false);

            if (data?.ContainsKey("grant_type") == true && data["grant_type"] == "password")
            {
                var username = data.ContainsKey("username") ? data["username"] : string.Empty;
                var password = data.ContainsKey("password") ? data["password"] : string.Empty;

                if (ValidateCredentials(username, password))
                {
                    context.Validated(username);
                }
                else
                {
                    context.Rejected();
                }
            }
            else
            {
                context.Rejected();
            }
        }

        private bool ValidateCredentials(string username, string password)
        {
            return username == "alanlviana";
        }
    }
}
