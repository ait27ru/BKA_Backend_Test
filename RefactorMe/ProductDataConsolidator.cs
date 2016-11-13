using System.Collections.Generic;
using System.Linq;
using RefactorMe.DontRefactor.Data;
using RefactorMe.DontRefactor.Models;

namespace RefactorMe
{
    public class ProductDataConsolidator
    {
        private readonly List<object> _repositories = new List<object>();

        public ProductDataConsolidator(params object[] repositories)
        {
            _repositories.AddRange(repositories);
        }

        private IEnumerable<Product> GetProducts<T>(IReadOnlyRepository<T> repository) where T : class
        {
            return from item in repository.GetAll()
                select ProductFactory.GetProduct(item);
        }

        private IEnumerable<Product> GetAllProducts(double exchangeRate = 1D)
        {
            var allProducts = new List<Product>();

            foreach (var repo in _repositories)
            {
                dynamic r = RepositoryFactory.GetRepository(repo);
                IEnumerable<Product> products = GetProducts(r);
                allProducts.AddRange(products.Select(p => p.ApplyExchangeRate(exchangeRate)));
            }

            return allProducts;
        }

        public List<Product> Get()
        {
            return GetAllProducts().ToList();
        }

        public List<Product> GetInUSDollars()
        {
            var usdRate = CurrencyService.GetUSDRate();
            return GetAllProducts(usdRate).ToList();
        }

        public List<Product> GetInEuros()
        {
            var eurRate = CurrencyService.GetEURRate();
            return GetAllProducts(eurRate).ToList();
        }
    }
}