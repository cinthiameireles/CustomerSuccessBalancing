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
        private Dictionary<int, int> CreateDictionaryScoresCostumer(List<Customer> customers, int maxScore)
        {
            Dictionary<int, int> totalCustomersByScore = new Dictionary<int, int>();

            foreach (var customer in customers)
            {
                if (customer.Score > maxScore) break;

                var valorAnterior = totalCustomersByScore.GetValueOrDefault(customer.Score);
                totalCustomersByScore[customer.Score] = valorAnterior + 1;
            }

            for (int i = 1; i <= maxScore; i++)
            {
                var iValue = totalCustomersByScore.GetValueOrDefault(i);
                var beforeIValue = totalCustomersByScore.GetValueOrDefault(i-1);
                totalCustomersByScore[i] = iValue + beforeIValue;
            }

            return totalCustomersByScore;
        }

        private Dictionary<int, bool> CreateDictionaryCsAway(List<int> customerSuccessAway)
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
            var customerSuccessOrdered = customerSuccess.OrderBy(item => item.Score).ToList();

            int csMaxScore = customerSuccessOrdered.Last().Score;

            var dictionaryCsAway = CreateDictionaryCsAway(customerSuccessAway);
            var dictionaryScoresByCustomers = CreateDictionaryScoresCostumer(customers, csMaxScore);

            int totalCustomersBefore = 0;
            int idCsMoreCustomers = 0, maxCustomers = 0;
            foreach (var cs in customerSuccessOrdered)
            {
                if (dictionaryCsAway.GetValueOrDefault(cs.Id)) continue;

                int totalCustomersCs = dictionaryScoresByCustomers[cs.Score] - totalCustomersBefore;
                totalCustomersBefore = dictionaryScoresByCustomers[cs.Score];

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
