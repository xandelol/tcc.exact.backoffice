namespace exac.backoffice.Models.Proxy
{
    public class SettingProxy
    {
        public string Value { get; set; }
        
        public string Key { get; set; }
        
        public SettingType Type { get; set; }
    }
    public enum SettingType
    {
        Int = 0,
        String = 1,
        Double = 2,
        DateTime = 3
    }
}