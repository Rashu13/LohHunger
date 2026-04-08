using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SarjuPos.API.Data;
using SarjuPos.API.DTOs;
using SarjuPos.API.Models;

namespace SarjuPos.API.Controllers
{
    [Authorize(Roles = "Owner")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("aggregate-sales")]
        public async Task<ActionResult<SalesReportDto>> GetAggregateSales()
        {
            var outlets = await _context.Outlets.ToListAsync();
            var orders = await _context.Orders.ToListAsync();

            var report = new SalesReportDto
            {
                TotalRevenue = orders.Sum(o => o.TotalAmount),
                TotalOrders = orders.Count,
                OutletPerformance = outlets.Select(o => new OutletSaleSummary
                {
                    OutletId = o.Id,
                    OutletName = o.Name,
                    TotalSales = orders.Where(ord => ord.OutletId == o.Id).Sum(ord => ord.TotalAmount),
                    OrderCount = orders.Count(ord => ord.OutletId == o.Id)
                }).ToList()
            };

            return Ok(report);
        }

        [HttpGet("outlet-comparison")]
        public async Task<ActionResult<IEnumerable<OutletSaleSummary>>> GetOutletComparison()
        {
            var performance = await _context.Outlets
                .Select(o => new OutletSaleSummary
                {
                    OutletId = o.Id,
                    OutletName = o.Name,
                    TotalSales = _context.Orders.Where(ord => ord.OutletId == o.Id).Sum(ord => ord.TotalAmount),
                    OrderCount = _context.Orders.Count(ord => ord.OutletId == o.Id)
                }).ToListAsync();

            return Ok(performance);
        }
    }
}
