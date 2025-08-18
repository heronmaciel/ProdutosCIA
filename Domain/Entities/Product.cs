using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sku {  get; set; }
        public decimal CostPrice { get; set; }

        public ICollection<Stock> Stocks { get; set; }
    }
}
