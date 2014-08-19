using MvcSolution.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSolution.DataLayer
{
    public class SalesContext:DbContext
    {
        public DbSet<SalesOrder> SalesOrders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SalesOrderConfiguration());
        }
    }
}
