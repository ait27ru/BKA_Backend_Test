using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactorMe.DontRefactor.Data;
using RefactorMe.DontRefactor.Data.Implementation;
using RefactorMe.DontRefactor.Models;
using StructureMap;

namespace RefactorMe.Tests
{
    [TestClass]
    public class ProductDataConsolidatorShould
    {
        private ProductDataConsolidator _consolidator;
        private Container _container;

        [TestInitialize]
        public void Initialise()
        {
            _container = new Container(cf =>
            {
                cf.For<IReadOnlyRepository<Lawnmower>>().Use<LawnmowerRepository>();
                cf.For<IReadOnlyRepository<PhoneCase>>().Use<PhoneCaseRepository>();
                cf.For<IReadOnlyRepository<TShirt>>().Use<TShirtRepository>();
            });

            _consolidator = new ProductDataConsolidator(
                _container.GetInstance<IReadOnlyRepository<Lawnmower>>(),
                _container.GetInstance<IReadOnlyRepository<PhoneCase>>(),
                _container.GetInstance<IReadOnlyRepository<TShirt>>());
        }

        [TestMethod]
        public void GetCorrectNumberOfAllRecords()
        {
            //Arrange
            var expected = new LawnmowerRepository().GetAll().Count() +
                           new PhoneCaseRepository().GetAll().Count() +
                           new TShirtRepository().GetAll().Count();

            //Act
            var actual = _consolidator.Get().Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetCorrectNumberOfLawnmowers()
        {
            //Arrange
            var expected = new LawnmowerRepository().GetAll().Count();

            //Act
            var actual = _consolidator.Get().Where(p => p.Type == ProductType.Lawnmower).Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetCorrectNumberOfPhoneCases()
        {
            //Arrange
            var expected = new PhoneCaseRepository().GetAll().Count();

            //Act
            var actual = _consolidator.Get().Where(p => p.Type == ProductType.PhoneCase).Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetCorrectNumberOfTShirts()
        {
            //Arrange
            var expected = new TShirtRepository().GetAll().Count();

            //Act
            var actual = _consolidator.Get().Where(p => p.Type == ProductType.TShirt).Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToUSDollars()
        {
            //Arrange
            var usdRate = CurrencyService.GetUSDRate();
            var expected = _consolidator.Get().Select(p => p.Price*.76D).ToArray();

            //Act
            var actual = _consolidator.GetInUSDollars().Select(p => p.Price).ToArray();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToEuros()
        {
            //Arrange
            var eurRate = CurrencyService.GetEURRate();
            var expected = _consolidator.Get().Select(p => p.Price*eurRate).ToArray();

            //Act
            var actual = _consolidator.GetInEuros().Select(p => p.Price).ToArray();

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}