using System;

namespace Liga.Backoffice.Lanxess.Models.Proxy
{
    public class TokenProxy
    {
        public string Token { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
    }
}