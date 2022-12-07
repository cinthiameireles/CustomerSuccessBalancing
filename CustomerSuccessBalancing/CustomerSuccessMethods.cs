using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSuccessBalancing
{
    public class CustomerSuccessMethods
    {
        public CustomerSuccess[] GenerateParsedManagers(CustomerSuccess[] managers, int[] unavailableManagers)
        {
            var unavailableManagersHash = new Dictionary<int, bool>();
            foreach (int id in unavailableManagers) unavailableManagersHash[id] = true;

            return managers.Where(manager => !unavailableManagersHash.GetValueOrDefault(manager.Id)).OrderBy(manager => manager.Score).ToArray();
        }

        //const generateParsedCustomers = (customers, managers) =>
        //{
        //    const maxScore = managers[managers.length - 1].score;

        //    return customers
        //        .filter((customer) => customer.score <= maxScore)
        //        .sort((c1, c2) => c2.score < c1.score ? 1 : -1)
        //}

        //const customerSuccessBalancing = (managers, customers, unavailableManagers) => {
        //    const parsedManagers = generateParsedManagers(managers, unavailableManagers);
        //    const parsedCustomers = generateParsedCustomers(customers, parsedManagers)

        //  let topManagerId = 0;
        //    let topManagerTotalCustomers = 0;
        //    let customerIndex = 0;

        //    for (const manager of parsedManagers) {
        //        let totalCustomers = 0;

        //        while (parsedCustomers[customerIndex]?.score <= manager.score)
        //        {
        //            totalCustomers++;
        //            customerIndex++;
        //        }

        //        if (totalCustomers > topManagerTotalCustomers)
        //        {
        //            topManagerId = manager.id;
        //            topManagerTotalCustomers = totalCustomers;
        //        }
        //        else if (totalCustomers === topManagerTotalCustomers)
        //        {
        //            topManagerId = 0;
        //        }
        //    }

        //    return topManagerId;
        //}
    }
}
