namespace exac.backoffice.Models
{
    public class AuthFailedViewModel
    {
        public bool AccessGranted { get; set; }
        
        public string Message { get; set; }
        
        public int Code { get; set; }
    }
}