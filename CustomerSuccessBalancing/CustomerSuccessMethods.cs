using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSuccessBalancing
{
    public class CustomerSuccessMethods
    {
        private int[] CreateArrayCostumersByScore(Customer[] customers, int maxScore)
        {
            int[] totalCustomersByScore = new int[maxScore + 1];

            foreach (var customer in customers)
            {
                if (customer.Score > maxScore) break;

                totalCustomersByScore[customer.Score]++;
            }

            for (int i = 1; i <= maxScore; i++)
            {
                totalCustomersByScore[i] += totalCustomersByScore[i - 1];
            }

            return totalCustomersByScore;
        }

        private Dictionary<int, bool> CreateDictionaryCsAway(int[] customerSuccessAway)
        {
            Dictionary<int, bool> dictionaryCsAway = new Dictionary<int, bool>();

            foreach (int cs in customerSuccessAway)
            {
                dictionaryCsAway[cs] = true;
            }

            return dictionaryCsAway;
        }

        public int CustomerSuccessBalancing(List<CustomerSuccess> customerSuccess, List<Customer> customers, List<int> customerSuccessAway)
        {
            var customerSuccessOrdered = customerSuccess.OrderBy(item => item.Score).ToArray();

            int csMaxScore = customerSuccessOrdered.Last().Score;

            var dictionaryCsAway = CreateDictionaryCsAway(customerSuccessAway.ToArray());
            var arrayCostumersByScore = CreateArrayCostumersByScore(customers.ToArray(), csMaxScore);

            int totalCustomersBefore = 0;
            int idCsMoreCustomers = 0, maxCustomers = 0;
            foreach (var cs in customerSuccessOrdered)
            {
                if (dictionaryCsAway.GetValueOrDefault(cs.Id)) continue;

                int totalCustomersCs = arrayCostumersByScore[cs.Score] - totalCustomersBefore;
                totalCustomersBefore = arrayCostumersByScore[cs.Score];

                if (totalCustomersCs > maxCustomers)
                {
                    idCsMoreCustomers = cs.Id;
                    maxCustomers = totalCustomersCs;
                }
                else if (totalCustomersCs == maxCustomers)
                {
                    idCsMoreCustomers = 0;
                }
            }

            return idCsMoreCustomers;
        }
    }
}
