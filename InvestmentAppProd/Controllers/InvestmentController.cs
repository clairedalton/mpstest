using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestmentAppProd.Api.Models;
using Microsoft.EntityFrameworkCore;
using InvestmentAppProd.Models;
using InvestmentAppProd.Data;
using InvestmentAppProd.Utilities;

namespace InvestmentAppProd.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InvestmentController : Controller
    {
        private readonly InvestmentDBContext _context;

        public InvestmentController(InvestmentDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Investment>> FetchAllInvestments()
        {
            try
            {
                var investments = _context.Investments.ToList();
                return Ok(Mappers.InvestmentsToResponses(investments, DateTime.Now));
            }
            catch (Exception e)
            {
                return NotFound(e.ToString());
            }
        }

        [HttpGet("name")]
        public ActionResult<Investment> FetchInvestment([FromQuery] string name)
        {
            try
            {
                var investment = _context.Investments.Find(name);
                if (investment == null)
                    return NotFound();

                return Ok(Mappers.InvestmentToResponse(investment, DateTime.Now));
            }
            catch (Exception e)
            {
                return NotFound(e.ToString());
            }
        }

        [HttpPost]
        public ActionResult<Investment> AddInvestment([FromBody] AddInvestmentRequest request)
        {
            try
            {
                if (request.StartDate > DateTime.Now)
                    return BadRequest("Investment Start Date cannot be in the future.");

                // TODO - Possibly migrate this to use AutoMapper or similar...
                var investment = new Investment(
                    request.Name,
                    request.StartDate ?? throw new Exception(),
                    request.InterestType ?? throw new Exception(),
                    request.InterestRate ?? throw new Exception(),
                    request.PrincipalAmount ?? throw new Exception());

                _context.ChangeTracker.Clear();
                _context.Investments.Add(investment);
                _context.SaveChanges();

                return CreatedAtAction("FetchAllInvestments", new { name = investment.Name } , Mappers.InvestmentToResponse(investment, DateTime.Now));
            }
            catch (DbUpdateException dbE)
            {
                return Conflict(dbE.ToString());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut("name")]
        public ActionResult UpdateInvestment([FromQuery] string name, [FromBody] AddInvestmentRequest request)
        {
            try
            {
                if (name != request.Name)
                    return BadRequest("Name does not match the Investment you are trying to update.");

                if (request.StartDate > DateTime.Now)
                    return BadRequest("Investment Start Date cannot be in the future.");

                // TODO - Possibly migrate this to use AutoMapper or similar...
                var investment = new Investment(
                    request.Name,
                    request.StartDate ?? throw new Exception(),
                    request.InterestType ?? throw new Exception(),
                    request.InterestRate ?? throw new Exception(),
                    request.PrincipalAmount ?? throw new Exception());

                _context.ChangeTracker.Clear();
                _context.Entry(investment).State = EntityState.Modified;
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.ToString());
            }
        }

        [HttpDelete("name")]
        public ActionResult DeleteInvestment([FromQuery] string name)
        {
            try
            {
                var investment = _context.Investments.Find(name);
                if (investment == null)
                {
                    return NotFound();
                }
                _context.ChangeTracker.Clear();
                _context.Investments.Remove(investment);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }
    }
}
