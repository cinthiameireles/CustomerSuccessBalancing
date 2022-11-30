using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSuccessBalancing
{
    public class Customer
    {
        public int Id { get; set; }
        public int Score { get; set; }

        public Customer(int id, int score)
        {
            Id = id;
            Score = score;
        }
    }
}
