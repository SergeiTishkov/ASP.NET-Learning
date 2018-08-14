using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public virtual Category Category { get; set; }
    }


    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime Created { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {


            using (var context = new Context())
            {
                var products = new Product()
                {
                    Id = 2,
                    ProductName = "Product 115",
                    Category = new Category()
                    {
                        Id = 3,
                        Name = "Category 99",
                        Created =  DateTime.UtcNow
                    }
                };
                
                var entry = context.Entry(products);
                entry.State = EntityState.Modified;

                context.SaveChanges();
            }



        }
    }
}
