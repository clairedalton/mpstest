using System;

namespace InvestmentAppProd.Api.Models
{
    public class InvestmentResponse
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public InterestType InterestType { get; set; }

        public double InterestRate { get; set; }

        public double PrincipalAmount { get; set; }

        public double CurrentValue { get; set; }
    }
}