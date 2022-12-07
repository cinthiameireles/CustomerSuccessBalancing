using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace CustomerSuccessBalancing
{
    [TestFixture]
    public class CustomerSuccessMethodsTests
    {
        private CustomerSuccessMethods _csMethods;

        [SetUp]
        public void SetUp()
        {
            _csMethods = new CustomerSuccessMethods();
        }

        private List<CustomerSuccess> MapCustomerSuccess(params int[] scores)
        {
            List<CustomerSuccess> entities = new List<CustomerSuccess>(scores.Length);
            for (int i = 0; i < scores.Length; i++)
            {
                entities.Add(new CustomerSuccess(i + 1, scores[i]));
            }
            return entities;
        }

        private List<Customer> MapCustomers(params int[] scores)
        {
            List<Customer> entities = new List<Customer>(scores.Length);
            for (int i = 0; i < scores.Length; i++)
            {
                entities.Add(new Customer(i + 1, scores[i]));
            }
            return entities;
        }

        private List<Customer> BuildSizeEntities(int size, int score)
        {
            List<Customer> entities = new List<Customer>(size);
            for (int i = 0; i < size; i++)
            {
                entities.Add(new Customer(i + 1, score));
            }
            return entities;
        }

        [Test]
        public void Scenario01()
        {
            List<CustomerSuccess> css = new List<CustomerSuccess>(4)
            {
                new CustomerSuccess(1, 60),
                new CustomerSuccess(2, 20),
                new CustomerSuccess(3, 95),
                new CustomerSuccess(4, 75)
            };
            List<Customer> customers = new List<Customer>(6){
                new Customer(1, 90),
                new Customer(2, 20),
                new Customer(3, 70),
                new Customer(4, 40),
                new Customer(5, 60),
                new Customer(6, 10)
            };

            List<int> csAway = new List<int>(2) { 2, 4 };

            Assert.That(_csMethods.CustomerSuccessBalancing(css, customers, csAway), Is.EqualTo(1));
        }

        [Test]
        public void Scenario02()
        {
            List<CustomerSuccess> css = MapCustomerSuccess(11, 21, 31, 3, 4, 5);
            List<Customer> customers = MapCustomers(10, 10, 10, 20, 20, 30, 30, 30, 20, 60);
            List<int> csAway = new List<int>();

            Assert.That(_csMethods.CustomerSuccessBalancing(css, customers, csAway), Is.EqualTo(0));
        }

        [Test]
        [Timeout(100)]
        public void Scenario03()
        {

            List<CustomerSuccess> css = MapCustomerSuccess(Enumerable.Range(1, 999).ToArray());
            List<Customer> customers = BuildSizeEntities(100000, 998);
            List<int> csAway = new List<int>(1) { 999 };

            Assert.That(_csMethods.CustomerSuccessBalancing(css, customers, csAway), Is.EqualTo(998));
        }

        [Test]
        public void Scenario04()
        {
            List<CustomerSuccess> css = MapCustomerSuccess(1, 2, 3, 4, 5, 6);
            List<Customer> customers = MapCustomers(10, 10, 10, 20, 20, 30, 30, 30, 20, 60);
            List<int> csAway = new List<int>();

            Assert.That(_csMethods.CustomerSuccessBalancing(css, customers, csAway), Is.EqualTo(0));
        }

        [Test]
        public void Scenario05()
        {
            List<CustomerSuccess> css = MapCustomerSuccess(100, 2, 3, 6, 4, 5);
            List<Customer> customers = MapCustomers(10, 10, 10, 20, 20, 30, 30, 30, 20, 60);
            List<int> csAway = new List<int>();

            Assert.That(_csMethods.CustomerSuccessBalancing(css, customers, csAway), Is.EqualTo(1));
        }

        [Test]
        public void Scenario06()
        {
            List<CustomerSuccess> css = MapCustomerSuccess(100, 99, 88, 3, 4, 5);
            List<Customer> customers = MapCustomers(10, 10, 10, 20, 20, 30, 30, 30, 20, 60);
            List<int> csAway = new List<int>() { 1, 3, 2 };

            Assert.That(_csMethods.CustomerSuccessBalancing(css, customers, csAway), Is.EqualTo(0));
        }

        [Test]
        public void Scenario07()
        {
            List<CustomerSuccess> css = MapCustomerSuccess(100, 99, 88, 3, 4, 5);
            List<Customer> customers = MapCustomers(10, 10, 10, 20, 20, 30, 30, 30, 20, 60);
            List<int> csAway = new List<int>() { 4, 5, 6 };

            Assert.That(_csMethods.CustomerSuccessBalancing(css, customers, csAway), Is.EqualTo(3));
        }

        [Test]
        public void Scenario08_Extra()
        {

            List<CustomerSuccess> css = MapCustomerSuccess(Enumerable.Range(1, 999).ToArray());
            List<Customer> customers = BuildSizeEntities(100000, 998);
            List<int> csAway = Enumerable.Range(1, 499).ToList();

            Assert.That(_csMethods.CustomerSuccessBalancing(css, customers, csAway), Is.EqualTo(998));
        }

        [Test]
        public void Scenario09_Extra()
        {

            List<CustomerSuccess> css = MapCustomerSuccess(Enumerable.Range(1, 999).ToArray());
            List<Customer> customers = BuildSizeEntities(100000, 998);
            List<int> csAway = Enumerable.Range(500, 999).ToList();

            Assert.That(_csMethods.CustomerSuccessBalancing(css, customers, csAway), Is.EqualTo(0));
        }

        [Test]
        [TestCase(15)]
        public void Scenario10_Extra_Performance(int msMax)
        {
            List<CustomerSuccess> css = MapCustomerSuccess(Enumerable.Range(1, 999).ToArray());
            List<Customer> customers = BuildSizeEntities(100000, 998);
            List<int> csAway = new List<int>(1) { 999 };

            int tries = 100;
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < tries; i++)
            {
                var x = _csMethods.CustomerSuccessBalancing(css, customers, csAway);
            }
            stopwatch.Stop();
            long mediaMs = stopwatch.ElapsedMilliseconds / tries;

            Console.WriteLine($"A média de tempo é {mediaMs}ms");

            Assert.That(mediaMs, Is.LessThanOrEqualTo(msMax));
        }
    }
}
