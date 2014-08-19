using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using MvcSolution.Model;

namespace MvcSolution.DataLayer
{
    public class SalesOrderConfiguration : EntityTypeConfiguration<SalesOrder>
    {
        public SalesOrderConfiguration()
        {
            Property(s => s.CustomerName).HasMaxLength(30).IsRequired();
            Property(s => s.PONumber).HasMaxLength(10).IsOptional();
            Ignore(s => s.ObjectState);
        }
    }
}
