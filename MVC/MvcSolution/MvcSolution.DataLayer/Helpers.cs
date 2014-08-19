using MvcSolution.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSolution.DataLayer
{
    public static class Helpers
    {
        public static EntityState ConvertState(ObjectState objectSate)
        {
            switch (objectSate)
            {                
                case ObjectState.Added:
                    return EntityState.Added;
                    
                case ObjectState.Modified:
                    return EntityState.Modified;
                    
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                    
                default:
                    return EntityState.Unchanged;  
            }
        }
    }
}
