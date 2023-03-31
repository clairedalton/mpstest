using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using InvestmentAppProd.Utilities;

namespace InvestmentAppProd.Models
{
	public class Investment
	{
		[Required]
		[Key]
		public string Name { get; set; }

		public DateTime StartDate { get; set; }

		public string InterestType { get; set; }

		public double InterestRate { get; set; }

		public double PrincipalAmount { get; set; }

		public double CurrentValue { get; set; } = 0;

		public Investment()
		{
		}

		public Investment(string name, DateTime startDate, string interestType, double rate, double principal)
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
			var months = 12 * (referenceTime.Year - StartDate.Year) + referenceTime.Month - StartDate.Month;
			var years = months / 12.0d;

			return InterestType switch
			{
				"Simple" => InterestCalculations.CalculateSimpleInterest(PrincipalAmount, annualRate, years),
				_ => InterestCalculations.CalculateCompoundInterest(PrincipalAmount, annualRate, years, 12)
			};
		}

		public void CalculateValue()
		{
			CurrentValue = GetValueAt(DateTime.Now);
		}
	}
}
