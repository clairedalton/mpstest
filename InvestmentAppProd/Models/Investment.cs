using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using InvestmentAppProd.Api;
using InvestmentAppProd.Utilities;

namespace InvestmentAppProd.Models
{
	public class Investment
	{
		[Required]
		[Key]
		public string Name { get; set; }

		public DateTime StartDate { get; set; }

		public InterestType InterestType { get; set; }

		public double InterestRate { get; set; }

		public double PrincipalAmount { get; set; }

		public Investment()
		{
		}

		public Investment(string name, DateTime startDate, InterestType interestType, double rate, double principal)
		{
			Name = name;
			StartDate = startDate;
			InterestType = interestType;
			InterestRate = rate;
			PrincipalAmount = principal;
		}

		public double GetValueAt(DateTime referenceTime)
		{
			// Interest rate is divided by 100.
			var annualRate = InterestRate / 100;
			
			// Technically, the counts the number of ends of months since the start date
			// Not entirely sure if this counts as "AND the period is rounded to the nearest month"
			var months = 12 * (referenceTime.Year - StartDate.Year) + referenceTime.Month - StartDate.Month;
			var years = months / 12.0d;

			return InterestType switch
			{
				InterestType.Simple => InterestCalculations.CalculateSimpleInterest(PrincipalAmount, annualRate, years),
				InterestType.Compound => InterestCalculations.CalculateCompoundInterest(PrincipalAmount, annualRate, years, 12),
				_ => throw new InvalidOperationException($"Unknown interest type {InterestType}")
			};
		}
	}
}
