using System;

namespace MyWebApp.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string Name  { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        public decimal? Weight { get; set; }
        public char? Class { get; set; }
        public char? Style { get; set; }       
        public DateTime SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int RowCount { get; set; }

    }
}