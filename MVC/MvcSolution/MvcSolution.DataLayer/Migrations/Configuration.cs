namespace MvcSolution.DataLayer.Migrations
{
    using MvcSolution.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcSolution.DataLayer.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;            
        }

        protected override void Seed(MvcSolution.DataLayer.SalesContext context)
        {
            context.SalesOrders.AddOrUpdate(
                so => so.CustomerName,
                new SalesOrder { CustomerName = "Adam", PONumber = "123" },
                new SalesOrder { CustomerName = "Michael" },
                new SalesOrder { CustomerName = "David", PONumber = "Acme 9" }
                );
        }
    }
}
