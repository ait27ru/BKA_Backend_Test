using System;
using RefactorMe.DontRefactor.Models;

namespace RefactorMe
{
    public static class ProductFactory
    {
        public static Product GetProduct(Lawnmower lawnmower)
        {
            if (lawnmower == null)
                throw new ArgumentNullException("lawnmower");

            return new Product
            {
                Id = lawnmower.Id,
                Name = lawnmower.Name,
                Price = lawnmower.Price,
                Type = ProductType.Lawnmower
            };
        }

        public static Product GetProduct(PhoneCase phoneCase)
        {
            if (phoneCase == null)
                throw new ArgumentNullException("phoneCase");

            return new Product
            {
                Id = phoneCase.Id,
                Name = phoneCase.Name,
                Price = phoneCase.Price,
                Type = ProductType.PhoneCase
            };
        }

        public static Product GetProduct(TShirt tshirt)
        {
            if (tshirt == null)
                throw new ArgumentNullException("tshirt");

            return new Product
            {
                Id = tshirt.Id,
                Name = tshirt.Name,
                Price = tshirt.Price,
                Type = ProductType.TShirt
            };
        }

        public static Product GetProduct<T>(T item)
        {
            var lawnmower = item as Lawnmower;

            if (lawnmower != null)
                return GetProduct(lawnmower);

            var phoneCase = item as PhoneCase;

            if (phoneCase != null)
                return GetProduct(phoneCase);

            var tshirt = item as TShirt;

            if (tshirt != null)
                return GetProduct(tshirt);

            throw new NotSupportedException($"GetProduct of {item.GetType().Name} is not supported.");
        }
    }
}