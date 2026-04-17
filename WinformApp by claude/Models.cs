// ============================================================
//  DTO/Models.cs  –  Plain data-transfer objects
// ============================================================
using System;

namespace WinformApp.DTO
{
    public class UserDTO
    {
        public int    UserID    { get; set; }
        public string UserName  { get; set; }
        public string Email     { get; set; }
        public string Password  { get; set; }   // hashed
        public bool   IsLocked  { get; set; }
    }

    public class ItemDTO
    {
        public int     ItemID      { get; set; }
        public string  ItemName    { get; set; }
        public string  Size        { get; set; }
        public string  Unit        { get; set; }
        public decimal UnitPrice   { get; set; }
        public int     Stock       { get; set; }
        public string  Description { get; set; }

        public override string ToString() => ItemName;
    }

    public class AgentDTO
    {
        public int    AgentID   { get; set; }
        public string AgentName { get; set; }
        public string Address   { get; set; }
        public string Phone     { get; set; }
        public string Email     { get; set; }

        public override string ToString() => AgentName;
    }

    public class OrderDTO
    {
        public int     OrderID     { get; set; }
        public DateTime OrderDate  { get; set; }
        public int     AgentID     { get; set; }
        public string  AgentName   { get; set; }   // joined field
        public decimal TotalAmount { get; set; }
        public string  Note        { get; set; }
    }

    public class OrderDetailDTO
    {
        public int     ID         { get; set; }
        public int     OrderID    { get; set; }
        public int     ItemID     { get; set; }
        public string  ItemName   { get; set; }    // joined field
        public int     Quantity   { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal LineTotal  => Quantity * UnitAmount;
    }

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
