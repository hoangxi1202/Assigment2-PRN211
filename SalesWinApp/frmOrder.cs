using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessObject.Models;
using BusinessObject.DataAccess;
using BusinessObject.Repository;

namespace SalesWinApp
{
    public partial class frmOrder : Form
    {
        public IOrderRepository orderRepository = new OrderRepository();
        public BindingSource source;
        public frmOrder()
        {
            InitializeComponent();
        }
        private void LoadOrderList()
        {
            try
            {
                source = new BindingSource();
                List<Order> listOrders = orderRepository.GetOrders();
                source.DataSource = listOrders;
                dgvOrderList.DataSource = source;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Order Management - Load List Order",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadOrderList();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void frmOrder_Load(object sender, EventArgs e)
        {

        }
    }
}
