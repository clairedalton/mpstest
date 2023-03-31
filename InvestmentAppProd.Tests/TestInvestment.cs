using System;
using InvestmentAppProd.Api;
using InvestmentAppProd.Models;
using NUnit.Framework;

namespace InvestmentAppProd.Tests
{
    [TestFixture]
    public class TestInvestment 
    {
        [Test]
        public void GetValueAt_WithSimpleInterest_ShouldCalculateValue()
        {
            var investment = new Investment("Investment", new DateTime(2020, 5, 4), InterestType.Simple, 3.875, 10000);
            
            // Same month, different day
            Assert.AreEqual(10000.00, investment.GetValueAt(new DateTime(2020, 5, 18)), 0.001);
            
            // 6 months - Same year, larger month
            Assert.AreEqual(10193.75, investment.GetValueAt(new DateTime(2020, 11, 12)), 0.001);

            // 9 months - Next year, smaller month
            Assert.AreEqual(10290.62, investment.GetValueAt(new DateTime(2021, 2, 1)), 0.001);

            // 18 months - Next year, larger month
            Assert.AreEqual(10581.25, investment.GetValueAt(new DateTime(2021, 11, 1)), 0.001);
            
            // 2 years
            Assert.AreEqual(10775.00, investment.GetValueAt(new DateTime(2022, 5, 1)));
        }
        
        
        [Test]
        public void GetValueAt_WithCompoundInterest_ShouldCalculateValue()
        {
            var investment = new Investment("Investment", new DateTime(2020, 5, 4), InterestType.Compound, 3.875, 10000);
            
            // Same month, different day
            Assert.AreEqual(10000.00, investment.GetValueAt(new DateTime(2020, 5, 18)), 0.001);
            
            // 6 months - Same year, larger month
            Assert.AreEqual(10195.32, investment.GetValueAt(new DateTime(2020, 11, 12)), 0.001);

            // 9 months - Next year, smaller month
            Assert.AreEqual(10294.41, investment.GetValueAt(new DateTime(2021, 2, 1)), 0.001);

            // 18 months - Next year, larger month
            Assert.AreEqual(10597.48, investment.GetValueAt(new DateTime(2021, 11, 1)), 0.001);
            
            // 2 years
            Assert.AreEqual(10804.47, investment.GetValueAt(new DateTime(2022, 5, 1)));
        }
        
    }
}