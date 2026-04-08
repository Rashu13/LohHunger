namespace SarjuPos.API.DTOs
{
    public class OutletSaleSummary
    {
        public int OutletId { get; set; }
        public string OutletName { get; set; } = string.Empty;
        public decimal TotalSales { get; set; }
        public int OrderCount { get; set; }
    }

    public class SalesReportDto
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public List<OutletSaleSummary> OutletPerformance { get; set; } = new List<OutletSaleSummary>();
    }
}
