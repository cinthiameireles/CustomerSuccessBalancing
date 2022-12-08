using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSuccessBalancing
{
    public class CustomerSuccessMethods
    {
        private CustomerSuccess[] GenerateParsedManagers(List<CustomerSuccess> managers, List<int> unavailableManagers)
        {
            var unavailableManagersHash = new Dictionary<int, bool>();
            foreach (int id in unavailableManagers) unavailableManagersHash[id] = true;

            return managers.Where(manager => !unavailableManagersHash.GetValueOrDefault(manager.Id)).OrderBy(manager => manager.Score).ToArray();
        }

        private Customer[] GenerateParsedCustomers(List<Customer> customers, CustomerSuccess[] managers)
        {
            int maxScore = managers[managers.Count() - 1].Score;

            return customers.Where(customer => customer.Score <= maxScore).OrderBy(customer => customer.Score).ToArray();
        }

        public int CustomerSuccessBalancing(List<CustomerSuccess> managers, List<Customer> customers, List<int> unavailableManagers)
        {
            CustomerSuccess[] parsedManagers = GenerateParsedManagers(managers, unavailableManagers);
            Customer[] parsedCustomers = GenerateParsedCustomers(customers, parsedManagers);

            var topManagerId = 0;
            var topManagerTotalCustomers = 0;
            var customerIndex = 0;
            var maxCustomers = parsedCustomers.Count();

            foreach (var manager in parsedManagers)
            {
                var totalCustomers = 0;

                while (customerIndex < maxCustomers && parsedCustomers[customerIndex].Score <= manager.Score)
                {
                    totalCustomers++;
                    customerIndex++;
                }

                if (totalCustomers > topManagerTotalCustomers)
                {
                    topManagerId = manager.Id;
                    topManagerTotalCustomers = totalCustomers;
                }
                else if (totalCustomers == topManagerTotalCustomers)
                {
                    topManagerId = 0;
                }
            }

            return topManagerId;
        }
    }
}
