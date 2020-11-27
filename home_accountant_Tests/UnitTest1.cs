using NUnit.Framework;
using home_accountant;
using System.Collections.Generic;
using Moq;
using System.Linq;
using home_accountant.Controllers;

namespace home_accountant_Tests
{
    public class Tests
    {
        #region MockProperties
        public IRepository<Income> MockIncomeRepository;
        public IRepository<Cost> MockCostRepository;
        public IRepository<Users> MockUsersRepository;

        private List<Income> GetAllTestIncomes()
        {
            var Incomes = new List<Income>
            {
                new Income { IncomeName="Salary", IncomeId=1, IncomeValue=1000, IncomeDate="01.01.2020", UserId=1 },
                new Income { IncomeName="Sell", IncomeId=2, IncomeValue=2000, IncomeDate="01.01.2020", UserId=2},
                new Income { IncomeName="Sell", IncomeId=3, IncomeValue=3000, IncomeDate="03.02.2020", UserId=2},
                new Income { IncomeName="Sell", IncomeId=4, IncomeValue=4000, IncomeDate="04.02.2020", UserId=1}
            };
            return Incomes;
        }

        private List<Income> GetPartTestIncomes()
        {
            var Incomes = new List<Income>
            {
                new Income { IncomeName="Salary", IncomeId=1, IncomeValue=1000, IncomeDate="01.01.2020", UserId=1 }
            };
            return Incomes;
        }

        private List<Cost> GetAllTestCosts()
        {
            var Costs = new List<Cost>
            {
                new Cost { CostName="Products", CostId=1, CostValue=2000, CostDate="01.01.2020", UserId=1 },
                new Cost { CostName="Products", CostId=2, CostValue=2000, CostDate="01.01.2020", UserId=2},
                new Cost { CostName="Service", CostId=3, CostValue=3000, CostDate="03.02.2020", UserId=2},
                new Cost { CostName="Service", CostId=4, CostValue=3000, CostDate="04.02.2020", UserId=1}
            };
            return Costs;
        }

        private List<Cost> GetPartTestCosts()
        {
            var Costs = new List<Cost>
            {
                new Cost { CostName="Service", CostId=3, CostValue=3000, CostDate="03.02.2020", UserId=2},
                new Cost { CostName="Service", CostId=4, CostValue=3000, CostDate="04.02.2020", UserId=1}
            };
            return Costs;
        }

        private List<Users> GetUsers()
        {
            var Users = new List<Users>
            {
                new Users { UserId=1, UserName="Mikhail", UserPassword="1234"},
                new Users { UserId=2, UserName="Ivan", UserPassword="4321"}
            };
            return Users;
        }

        private void SetMockRepository()
        {
            Mock<IRepository<Income>> mockIncomeRepository = new Mock<IRepository<Income>>();
            List<Income> IncomesFull = GetAllTestIncomes();

            mockIncomeRepository.Setup(ir => ir.GetAll()).Returns(IncomesFull);
            this.MockIncomeRepository = mockIncomeRepository.Object;
            ////
            Mock<IRepository<Cost>> mockCostRepository = new Mock<IRepository<Cost>>();
            List<Cost> CostsFull = GetAllTestCosts();

            mockCostRepository.Setup(cr => cr.GetAll()).Returns(CostsFull);
            this.MockCostRepository = mockCostRepository.Object;
            ////
            Mock<IRepository<Users>> mockUsersRepository = new Mock<IRepository<Users>>();
            List<Users> Users = GetUsers();

            mockUsersRepository.Setup(cr => cr.GetAll()).Returns(Users);
            this.MockUsersRepository = mockUsersRepository.Object;
        }
        #endregion

        [Test]
        public void TestIndexController()
        {
            string expectedString = "It's Home Accountant.\n\n" +
                "Available endpoints:\n" +
                "GET api/Cost/List - Show list of costs\n" +
                "GET api/Cost/List/{TypeName} - Show list of costs by TypeName\n" +
                "GET api/Cost/SumDay/{Date} - Show sum of day by Date\n" +
                "GET api/Cost/SumMonth/{MonthDate} - Show sum of month by Date\n" +
                "POST api/Cost/Add - Add new cost to database\n" +
                "DELETE api/Cost/Delete/{TypeName} - Delete cost from database\n\n" +
                "GET api/Income/List - Show list of incomes\n" +
                "GET api/Income/List/{TypeName} - Show list of incomes by TypeName\n" +
                "GET api/Income/SumDay/{Date} - Show sum of day by Date\n" +
                "GET api/Income/SumMonth/{MonthDate} - Show sum of month by Date\n" +
                "POST api/Income/Add - Add new income to database\n" +
                "DELETE api/Income/Delete/{TypeName} - Delete income from database";

            IndexController indexController = new IndexController();
            string testString = indexController.Index();
            Assert.AreEqual(expectedString, testString);
        }

        [Test]
        public void TestCostController()
        {
            SetMockRepository();

            UnitOfWork unitOfWork = new UnitOfWork(MockIncomeRepository, MockCostRepository, MockUsersRepository);
            CostController costController = new CostController(unitOfWork);

            var testListAll = costController.GetCostList();
            var testListPart = costController.GetCostListByName("Service");

            Assert.AreEqual(GetAllTestCosts().Count, testListAll.ToList().Count);
            Assert.AreEqual(GetPartTestCosts().Count, testListPart.ToList().Count);

            string testSumDay = costController.GetDaySumForDate("01.01.2020");
            string testSumMonth = costController.GetMonthSumForDate("02.2020");

            Assert.AreEqual("4000", testSumDay);
            Assert.AreEqual("6000", testSumMonth);
        }

        [Test]
        public void TestIncomeController()
        {
            SetMockRepository();

            UnitOfWork unitOfWork = new UnitOfWork(MockIncomeRepository, MockCostRepository, MockUsersRepository);
            IncomeController incomeController = new IncomeController(unitOfWork);

            var testListAll = incomeController.GetIncomeList();
            var testListPart = incomeController.GetIncomeListByName("Salary");

            Assert.AreEqual(GetAllTestIncomes().Count, testListAll.ToList().Count);
            Assert.AreEqual(GetPartTestIncomes().Count, testListPart.ToList().Count);

            string testSumDay = incomeController.GetDaySumForDate("01.01.2020");
            string testSumMonth = incomeController.GetMonthSumForDate("02.2020");

            Assert.AreEqual("3000", testSumDay);
            Assert.AreEqual("7000", testSumMonth);
        }
    }
}