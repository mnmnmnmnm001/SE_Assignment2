using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using WindowsFormsApp.DAL;

namespace WindowsFormsApp.BLL
{
    public class UserBLL
    {
        private readonly UserDAL _dal = new UserDAL();

        public static string HashMD5(string plain)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(plain));
                var sb = new StringBuilder();
                foreach (var b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        /// <summary>Returns logged-in user or null if credentials wrong / account locked.</summary>
        public UserDTO Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;
            return _dal.Login(username, HashMD5(password));
        }

        public List<UserDTO> GetAll()       => _dal.GetAll();
        public void ToggleLock(int id, bool locked) => _dal.ToggleLock(id, locked);

        public (bool ok, string msg) Register(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) return (false, "Username is required.");
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return (false, "Password must be at least 6 characters.");
            var dto = new UserDTO { UserName = username, Email = email,
                                    Password = HashMD5(password), IsLocked = false };
            _dal.Insert(dto);
            return (true, "OK");
        }
    }
}


// ============================================================
//  BLL/ItemBLL.cs
// ============================================================
namespace WinformApp.BLL
{
    using System.Collections.Generic;
    using WinformApp.DAL;
    using WinformApp.DTO;

    public class ItemBLL
    {
        private readonly ItemDAL _dal = new ItemDAL();

        public List<ItemDTO>     GetAll()              => _dal.GetAll();
        public ItemDTO           GetByID(int id)       => _dal.GetByID(id);
        public List<BestItemDTO> GetBestItems(int topN)       => _dal.GetBestItems(topN);
        public List<BestItemDTO> GetItemsByAgent(int agentID) => _dal.GetItemsByAgent(agentID);

        public (bool ok, string msg) Save(ItemDTO item)
        {
            if (string.IsNullOrWhiteSpace(item.ItemName)) return (false, "Item name required.");
            if (item.UnitPrice < 0)                       return (false, "Unit price must be >= 0.");
            if (item.Stock < 0)                           return (false, "Stock must be >= 0.");

            if (item.ItemID == 0) _dal.Insert(item);
            else                  _dal.Update(item);
            return (true, "Saved successfully.");
        }

        public (bool ok, string msg) Delete(int id)
        {
            try { _dal.Delete(id); return (true, "Deleted."); }
            catch { return (false, "Cannot delete – item is referenced in orders."); }
        }
    }
}


// ============================================================
//  BLL/AgentBLL.cs
// ============================================================
namespace WinformApp.BLL
{
    using System.Collections.Generic;
    using WinformApp.DAL;
    using WinformApp.DTO;

    public class AgentBLL
    {
        private readonly AgentDAL _dal = new AgentDAL();

        public List<AgentDTO>         GetAll()                    => _dal.GetAll();
        public AgentDTO               GetByID(int id)             => _dal.GetByID(id);
        public List<AgentPurchaseDTO> GetAgentsByItem(int itemID) => _dal.GetAgentsByItem(itemID);
        public List<AgentPurchaseDTO> GetPurchaseSummary()        => _dal.GetAgentPurchaseSummary();

        public (bool ok, string msg) Save(AgentDTO a)
        {
            if (string.IsNullOrWhiteSpace(a.AgentName)) return (false, "Agent name required.");
            if (string.IsNullOrWhiteSpace(a.Address))   return (false, "Address required.");

            if (a.AgentID == 0) _dal.Insert(a);
            else                _dal.Update(a);
            return (true, "Saved successfully.");
        }

        public (bool ok, string msg) Delete(int id)
        {
            try { _dal.Delete(id); return (true, "Deleted."); }
            catch { return (false, "Cannot delete – agent has existing orders."); }
        }
    }
}


// ============================================================
//  BLL/OrderBLL.cs
// ============================================================
namespace WinformApp.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WinformApp.DAL;
    using WinformApp.DTO;

    public class OrderBLL
    {
        private readonly OrderDAL _dal = new OrderDAL();

        public List<OrderDTO>       GetAll()           => _dal.GetAll();
        public OrderDTO             GetByID(int id)    => _dal.GetByID(id);
        public List<OrderDetailDTO> GetDetails(int id) => _dal.GetDetails(id);

        public (bool ok, string msg, int orderID) Save(OrderDTO order,
                                                        List<OrderDetailDTO> details)
        {
            if (order.AgentID == 0)      return (false, "Select an agent.", 0);
            if (details == null || details.Count == 0)
                return (false, "Add at least one item.", 0);
            if (details.Any(d => d.Quantity <= 0))
                return (false, "Quantity must be > 0.", 0);

            order.TotalAmount = details.Sum(d => d.LineTotal);

            if (order.OrderID == 0)
            {
                int newID = _dal.InsertWithDetails(order, details);
                return (true, "Order saved.", newID);
            }
            else
            {
                _dal.UpdateWithDetails(order, details);
                return (true, "Order updated.", order.OrderID);
            }
        }

        public (bool ok, string msg) Delete(int id)
        {
            try { _dal.Delete(id); return (true, "Deleted."); }
            catch { return (false, "Error deleting order."); }
        }
    }
}
