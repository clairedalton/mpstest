using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentAppProd.Api.Models;
using InvestmentAppProd.Models;

namespace InvestmentAppProd.Utilities
{
    public static class Mappers
    {
        public static InvestmentResponse InvestmentToResponse(Investment investment, DateTime now)
        {
            return new InvestmentResponse
            {
                Name = investment.Name,
                StartDate = investment.StartDate,
                InterestType = investment.InterestType,
                InterestRate = investment.InterestRate,
                PrincipalAmount = investment.PrincipalAmount,
                CurrentValue = investment.GetValueAt(now)
            };
        }
        public static IEnumerable<InvestmentResponse> InvestmentsToResponses(IEnumerable<Investment> investments, DateTime now)
        {
            return investments.Select(investment => InvestmentToResponse(investment, now));
        }
        
    }
}