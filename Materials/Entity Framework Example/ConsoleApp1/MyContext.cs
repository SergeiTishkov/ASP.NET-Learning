using System.Data.Entity;

namespace ConsoleApp1
{
    public class MyContext : DbContext
    {
        public IDbSet<Product> Products { get; set; }
        // @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\TestDB.mdf;Integrated Security=True;Connect Timeout=30"
        // Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=True
        public MyContext() : base(@"name=ConsoleApp1.Properties.Settings.ConStrByServer")
        {
        }
    }
}
