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
            JoinRegistry.Add<Category, Product>();
            JoinRegistry.Add<Product, LocalProduct>();
            JoinRegistry.Add<Product, ProductOption>();
            JoinRegistry.Add<ProductOption, ProductOptionValue>();

            var query = new GenericQuery<Product>().Join<Product, Category>().Join<Product, LocalProduct>();
            Console.Out.Write(query.ToSql());

            var query2 = new GenericQuery<Product>().Join<Product, ProductOption>().Join<ProductOption, ProductOptionValue>();
            Console.Out.Write(query2.ToSql());

        }
    }
}
