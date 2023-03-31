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
using InvestmentAppProd.Services;
using InvestmentAppProd.Utilities;
using Microsoft.Extensions.Internal;

namespace InvestmentAppProd.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InvestmentController : Controller
    {
        private readonly InvestmentDBContext _context;
        private readonly IWallClock _clock;

        public InvestmentController(InvestmentDBContext context, IWallClock clock)
        {
            _context = context;
            _clock = clock;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InvestmentResponse>> FetchAllInvestments()
        {
            try
            {
                var investments = _context.Investments.ToList();
                return Ok(Mappers.InvestmentsToResponses(investments, _clock.Now));
            }
            catch (Exception e)
            {
                return NotFound(e.ToString());
            }
        }

        [HttpGet("name")]
        public ActionResult<InvestmentResponse> FetchInvestment([FromQuery] string name)
        {
            try
            {
                var investment = _context.Investments.Find(name);
                if (investment == null)
                    return NotFound();

                return Ok(Mappers.InvestmentToResponse(investment, _clock.Now));
            }
            catch (Exception e)
            {
                return NotFound(e.ToString());
            }
        }

        [HttpPost]
        public ActionResult<InvestmentResponse> AddInvestment([FromBody] AddInvestmentRequest request)
        {
            try
            {
                if (request.StartDate > _clock.Now)
                    return BadRequest("Investment Start Date cannot be in the future.");

                var investment = CreateInvestmentFromRequest(request);

                _context.ChangeTracker.Clear();
                _context.Investments.Add(investment);
                _context.SaveChanges();

                return CreatedAtAction("FetchInvestment",
                    new { name = investment.Name },
                    Mappers.InvestmentToResponse(investment, _clock.Now));
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

                if (request.StartDate > _clock.Now)
                    return BadRequest("Investment Start Date cannot be in the future.");
                
                var investment = CreateInvestmentFromRequest(request);
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
                    // Depending on how this API is intended to be used, it may be better to return NoContent here
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

        private static Investment CreateInvestmentFromRequest(AddInvestmentRequest request)
        {
            // TODO - Possibly migrate this to use AutoMapper or similar...
            var investment = new Investment(
                request.Name,
                request.StartDate ?? throw new Exception(),
                request.InterestType ?? throw new Exception(),
                request.InterestRate ?? throw new Exception(),
                request.PrincipalAmount ?? throw new Exception());
            return investment;
        }
    }
}
