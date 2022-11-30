using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSuccessBalancing
{
    public class CustomerSuccessMethods
    {
        public int CustomerSuccessBalancing(List<CustomerSuccess> customerSuccess, List<Customer> customers, List<int> customerSuccessAway)
        {
            List<CustomerSuccess> csAvailable = customerSuccess.Where(csItem => !customerSuccessAway.Contains(csItem.Id)).OrderBy(csItem => csItem.Score).ToList();
            var csMostCostumers = new { IdCs = 0, TotalCustomers = 0 };

            foreach(var cs in csAvailable)
            {
                int totalCustomers = customers.RemoveAll(item => item.Score <= cs.Score);

                if (totalCustomers > csMostCostumers.TotalCustomers) csMostCostumers = new { IdCs = cs.Id, TotalCustomers = totalCustomers };
                else if (totalCustomers == csMostCostumers.TotalCustomers) csMostCostumers = new { IdCs = 0, TotalCustomers = totalCustomers };
            }

            return csMostCostumers.IdCs;
        }
    }
}
