using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSuccessBalancing
{
    public class CustomerSuccessMethods
    {
        private int? FindAssignedCs(List<CustomerSuccess> customerSuccess, int customerScore)
        {
            int start = 0, end = customerSuccess.Count - 1;
            CustomerSuccess assignedCs = null;

            while (start <= end)
            {
                int pivot = (int)Math.Floor((double)(start + end) / 2);
                var pivotCs = customerSuccess[pivot];

                int pivotScore = pivotCs.Score;
                int? assignedCsScore = assignedCs?.Score;

                if (customerScore == pivotScore) return pivotCs.Id;

                if (customerScore > pivotScore) start = pivot + 1;
                else if (customerScore < pivotScore)
                {
                    end = pivot - 1;

                    if (assignedCsScore > pivotScore || assignedCsScore == null) assignedCs = pivotCs;
                }
            }

            return assignedCs?.Id;
        }

        private Dictionary<int, bool> GenerateUnavailableCsHash(List<int> customerSuccessAway) => customerSuccessAway.ToDictionary(x => x, x => true);

        private List<CustomerSuccess> GenerateParsedCs(List<CustomerSuccess> customerSuccess, Dictionary<int, bool> unavailableCsHash) => customerSuccess.Where(x => !unavailableCsHash.ContainsKey(x.Id)).OrderBy(x => x.Score).ToList();

        public int CustomerSuccessBalancing(List<CustomerSuccess> customerSuccess, List<Customer> customers, List<int> customerSuccessAway)
        {
            var unavailableCsHash = GenerateUnavailableCsHash(customerSuccessAway);
            var parsedCs = GenerateParsedCs(customerSuccess, unavailableCsHash);
            var csTotalCustomersHash = new Dictionary<int, int>();

            int topCsId = 0;
            bool isTie = false;

            foreach (var customer in customers)
            {
                var assignedCsId = FindAssignedCs(parsedCs, customer.Score);

                if (assignedCsId != null)
                {
                    int assignedCsTotalCustomers;
                    csTotalCustomersHash.TryGetValue(assignedCsId.Value, out assignedCsTotalCustomers);
                    assignedCsTotalCustomers++;

                    int topCsTotalCustomers;
                    csTotalCustomersHash.TryGetValue(topCsId, out topCsTotalCustomers);

                    if (assignedCsTotalCustomers == topCsTotalCustomers)
                    {
                        isTie = true;
                    }
                    else if(assignedCsTotalCustomers > topCsTotalCustomers)
                    {
                        topCsId = assignedCsId.Value;
                        isTie = false;
                    }

                    csTotalCustomersHash[assignedCsId.Value] = assignedCsTotalCustomers;
                }
            }

            if (isTie) return 0;

            return topCsId;
        }
    }
}
