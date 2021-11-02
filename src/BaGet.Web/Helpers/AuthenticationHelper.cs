using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace BaGet.Web
{

    public class AuthenticationHelper
    {
        private readonly string challengeHeader = "WWW-Authenticate";
        private readonly string challengeMessage = "Basic realm = \"Secure Area\"";
        private readonly int unauthorizedCode = 401;
        private readonly string unauthorizedMessage = "Status Code: 401; Unauthorized";

        public IActionResult getAuthenticationError(IHeaderDictionary headers, HttpResponse response)
        {

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BAGET_WEB_USER")) || string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BAGET_WEB_PASSWORD")))
            {
                return null;
            }

            var base64EncodedString = headers["Authorization"];
            if (string.IsNullOrEmpty(base64EncodedString) || string.IsNullOrWhiteSpace(base64EncodedString))
            {
                response.Headers.Add(challengeHeader, challengeMessage);
                return new ObjectResult(unauthorizedMessage) { StatusCode = unauthorizedCode };
            }
            var credentials = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(base64EncodedString.ToString().Substring(6))).Split(':');

            if (!(credentials[0] == Environment.GetEnvironmentVariable("BAGET_WEB_USER") && credentials[1] == Environment.GetEnvironmentVariable("BAGET_WEB_PASSWORD")))
            {
                response.Headers.Add(challengeHeader, challengeMessage);
                return new ObjectResult(unauthorizedMessage) { StatusCode = unauthorizedCode };
            }

            return null;
        }
    }
}
