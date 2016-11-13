namespace RefactorMe
{
    public static class CurrencyService
    {
        public static double GetUSDRate()
        {
            return 0.76D;
        }

        public static double GetEURRate()
        {
            return 0.67D;
        }
    }

    public static class ProductType
    {
        public const string Lawnmower = "Lawnmower";
        public const string PhoneCase = "Phone Case";
        public const string TShirt = "T-Shirt";
    }
}