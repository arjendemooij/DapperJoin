using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDapperJoin
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseProductId { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public IEnumerable<ProductLocal> LocalProduct { get; set; }
        public IEnumerable<ProductProperty> ProductProperties { get; set; }
        public IEnumerable<ProductOption> ProductOptions { get; set; }
    }
}
