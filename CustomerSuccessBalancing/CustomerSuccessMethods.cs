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
            var customerSuccessOrdered = customerSuccess.OrderBy(item => item.Score).ToArray();
            var customersOrdered = customers.OrderBy(item => item.Score).ToArray();

            int idCsMostCostumers = 0, totalCustomersCsMostCustomers = 0, totalCustomersAnalyzed = 0;

            for (int i = 0; i < customerSuccessOrdered.Count(); i++)
            {
                if (customerSuccessAway.Contains(customerSuccessOrdered[i].Id)) continue;

                int totalCustomer = 0;
                for (int j = totalCustomersAnalyzed; j < customersOrdered.Count() && customersOrdered[j].Score <= customerSuccessOrdered[i].Score; j++, totalCustomersAnalyzed++, totalCustomer++) ;

                if (totalCustomer > totalCustomersCsMostCustomers)
                {
                    totalCustomersCsMostCustomers = totalCustomer;
                    idCsMostCostumers = customerSuccessOrdered[i].Id;
                }
                else if (totalCustomer == totalCustomersCsMostCustomers)
                {
                    idCsMostCostumers = 0;
                }
            }

            return idCsMostCostumers;
        }
    }
}
