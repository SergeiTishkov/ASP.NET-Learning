using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics;
using ConsoleApp1.Migrations;

namespace ConsoleApp1
{
    public class Context: DbContext
    {

        public Context()
        {
            Database.Log += s => Debug.WriteLine(s);
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
        }
        public IDbSet<Product> Products { get; set; }
        
    }


    class MyClass :EntityTypeConfiguration<Product>
    {
        public MyClass()
        {
            
        }
    }
}