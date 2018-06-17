using Liga.Backoffice.Lanxess.Enums;

namespace Liga.Backoffice.Lanxess.Models.Proxy
{
    public class SettingProxy
    {
        public string Value { get; set; }
        
        public string Key { get; set; }
        
        public SettingType Type { get; set; }
    }
}