using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObject.Models;

namespace BusinessObject.DataAccess
{
    public class OrderDAO
    {
        private OrderDAO() { }
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Order> GetOrders()
        {
            List<Order> listOrders = new List<Order>();
            try
            {
                FStoreContext DbContext = new FStoreContext();
                listOrders = DbContext.Orders.ToList();
            }
            catch (Exception)
            {
                throw new Exception("Get list orders unsuccessfully");
            }
            return listOrders;
        }

        public void AddNewOrder(Order order)
        {
            try
            {
                FStoreContext DbContext = new FStoreContext();
                //1. thực hiện thêm trên DbContext
                DbContext.Orders.Add(order);
                //2. thực hiện thay đổi dưới database
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Add a new order unsuccessfully");
            }
        }

        public void DeleteOrder(int orderID)
        {
            try
            {
                FStoreContext DbContext = new FStoreContext();
                Order? order = DbContext.Orders.
                    SingleOrDefault(order => order.OrderId == orderID);
                DbContext.Orders.Remove(order);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Delete a order unsuccessfully");
            }
        }

        public void UpdateOrder(Order order)
        {
            try
            {
                FStoreContext DbContext = new FStoreContext();
                DbContext.Entry<Order>(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Update a order unsuccessfully");
            }
        }
    }

    
}
