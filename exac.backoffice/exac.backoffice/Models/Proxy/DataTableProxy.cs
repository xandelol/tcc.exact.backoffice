using System.Collections.Generic;

namespace Liga.Backoffice.Lanxess.Models.Proxy
{
    public class DataTableProxy<T> 
    {
        public string Draw { get; set; }

        public int RecordsFiltered { get; set; }
        
        public int RecordsTotal { get; set; }
        
        public ICollection<T> Data { get; set; }
  
    }
}