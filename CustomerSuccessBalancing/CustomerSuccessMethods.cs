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
            List<CustomerSuccess> customerSuccessCopy = customerSuccess.OrderBy(item => item.Score).ToList();
            List<Customer> customersCopy = customers.OrderBy(item => item.Score).ToList();

            int idCsMostCostumer = 0, totalCustomersCsMostCustomer = 0, totalGeral = 0;

            for (int i = 0; i < customerSuccessCopy.Count; i++)
            {
                if (customerSuccessAway.Contains(customerSuccessCopy[i].Id)) continue;

                int totalCustomer = 0;
                for (int j = totalGeral; j < customersCopy.Count() && customersCopy[j].Score <= customerSuccessCopy[i].Score; j++, totalGeral++, totalCustomer++) ;

                if (totalCustomer > totalCustomersCsMostCustomer)
                {
                    totalCustomersCsMostCustomer = totalCustomer;
                    idCsMostCostumer = customerSuccessCopy[i].Id;
                }
                else if (totalCustomer == totalCustomersCsMostCustomer)
                {
                    idCsMostCostumer = 0;
                }
            }

            return idCsMostCostumer;
        }
    }
}
