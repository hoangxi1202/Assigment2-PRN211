using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using BusinessObject.Models;

namespace BusinessObject.Repository
{
    public interface IOrderRepository
    {
        public List<Order> GetOrders();
        public void AddNewOrder(Order order);
        public void UpdateOrder(Order order);
        public void DeleteOrder(int orderID);
    }
}
