using System;

namespace InvestmentAppProd.Utilities
{
    public static class InterestCalculations
    {
        public static double CalculateSimpleInterest(double principalAmount, double annualRate, double years)
        {
            var accruedAmount = principalAmount * (1 + (annualRate * years));
            return RoundCurrency(accruedAmount);
        }

        public static double CalculateCompoundInterest(double principalAmount, double annualRate, double years, int periodsPerYear)
        {
           var accruedAmount = principalAmount * Math.Pow((1 + (annualRate / periodsPerYear)), (periodsPerYear * years));
           return RoundCurrency(accruedAmount);
        }

        private static double RoundCurrency(double amount)
        {
            return Math.Round(amount, 2);
        }
    }
}