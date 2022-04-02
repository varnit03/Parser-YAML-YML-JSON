using System.Collections.Generic;

namespace Garter.Models
{
    public class SoftwareAdviceModel
    {
        public List<Products> products { get; set; }
    }

    public class Products
    {
        public List<string> categories { get; set; }
        public string title { get; set; }
        public string twitter { get; set; }
    }
}
