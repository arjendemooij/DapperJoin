using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDapperJoin
{
    public class ProductOptionValue
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public int ProductOptionId { get; set; }
        public ProductOption ProductOption { get; set; }

    }
}
