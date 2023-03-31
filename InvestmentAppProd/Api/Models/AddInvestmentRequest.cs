#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

namespace InvestmentAppProd.Api.Models
{
    public class AddInvestmentRequest
    {
        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [Required]
        public InterestType? InterestType { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double? InterestRate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double? PrincipalAmount { get; set; }
    }
}