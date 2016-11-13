using RefactorMe.DontRefactor.Models;

namespace RefactorMe
{
    public static class ProductExtension
    {
        public static Product ApplyExchangeRate(this Product product, double exchangeRate)
        {
            if (!exchangeRate.Equals(1D))
            {
                product.Price *= exchangeRate;
            }
            return product;
        }
    }
}