using System;
namespace WindowsFormsApp.DAL
{
    // Used for filter / report queries
    public class BestItemDTO
    {
        public int     ItemID       { get; set; }
        public string  ItemName     { get; set; }
        public int     TotalQty     { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class AgentPurchaseDTO
    {
        public int     AgentID     { get; set; }
        public string  AgentName   { get; set; }
        public string  Address     { get; set; }
        public int     OrderCount  { get; set; }
        public decimal TotalSpent  { get; set; }
    }
}