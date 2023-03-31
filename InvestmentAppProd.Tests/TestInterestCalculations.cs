using InvestmentAppProd.Utilities;
using NUnit.Framework;

namespace InvestmentAppProd.Tests
{
    [TestFixture]
    public class TestInterestCalculations
    {
        [Test]
        public void CalculateSimpleInterest_ShouldApplySimpleInterest()
        {
            // Calculations verified against:
            // https://www.calculatorsoup.com/calculators/financial/simple-interest-plus-principal-calculator.php
            var oneYearResult = InterestCalculations.CalculateSimpleInterest(10000.0d, 0.03875d, 1);
            Assert.AreEqual(10387.50d, oneYearResult);

            var fiveYearResult = InterestCalculations.CalculateSimpleInterest(10000.0d, 0.03875d, 5);
            Assert.AreEqual(11937.50d, fiveYearResult);

            var sixMonthResult = InterestCalculations.CalculateSimpleInterest(10000.0d, 0.03875d, 0.5);
            Assert.AreEqual(10193.75d, sixMonthResult);
        }

        [Test]
        public void CalculateCompoundInterest_ShouldApplyCompoundInterest_Monthly()
        {
            // Calculations verified against:
            // https://www.calculatorsoup.com/calculators/financial/compound-interest-calculator.php
            var oneYearResult = InterestCalculations.CalculateCompoundInterest(10000d, 0.03875d, 1, 12);
            Assert.AreEqual(10394.46d, oneYearResult);

            var fiveYearResult = InterestCalculations.CalculateCompoundInterest(10000d, 0.03875d, 5, 12);
            Assert.AreEqual(12134.14d, fiveYearResult);
        }

        [Test]
        public void CalculateCompoundInterest_ShouldApplyCompoundInterest_BiMonthly()
        {
            // Calculations verified against:
            // https://www.calculatorsoup.com/calculators/financial/compound-interest-calculator.php
            var fiveYearResult = InterestCalculations.CalculateCompoundInterest(10000d, 0.03875d, 5, 6);
            Assert.AreEqual(12130.37d, fiveYearResult);
        }
    }
}