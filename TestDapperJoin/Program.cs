using DapperJoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDapperJoin
{
    class Program
    {
        static void Main(string[] args)
        {
            RelationRegistry.Add<Category, Product>();
            RelationRegistry.Add<Product, ProductLocal>();
            RelationRegistry.Add<Product, ProductOption>();
            RelationRegistry.Add<ProductOption, ProductOptionValue>();
            RelationRegistry.Add<ProductLocal, ProductLocal>();

            var query = new Query<ProductLocal>().Join<ProductLocal, ProductLocal>();
            Console.Out.Write(query.ToSql());
        }
    }
}
