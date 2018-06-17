using System;

namespace exac.backoffice.Models.Proxy
{
    public class TokenProxy
    {
        public string Token { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
    }
}