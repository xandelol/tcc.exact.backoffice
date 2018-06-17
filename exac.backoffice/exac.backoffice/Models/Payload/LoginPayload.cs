namespace exac.backoffice.Models.Payload
{
    public class LoginPayload
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ClientAccessType Type { get; set; }
    }

    public enum ClientAccessType
    {
        App,
        Backoffice
    }
}