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
using System.Text.RegularExpressions;

namespace SalesWinApp
{
    public partial class frmOrder : Form
    {
        public IOrderRepository orderRepository = new OrderRepository();
        public BindingSource? source;
        public frmOrder()
        {
            InitializeComponent();
        }
        private void LoadOrderList()
        {
            try
            {
                var orders = orderRepository.GetOrders().OrderByDescending(x => x.OrderId);
                source = new BindingSource();
                List<Order> listOrders = orderRepository.GetOrders();
                source.DataSource = orders;
                dgvOrderList.DataSource = source;
                dgvOrderList.Columns[6].Visible = false;
                dgvOrderList.Columns[7].Visible = false;
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
            if (btnNew.Text.Equals("New"))
            {
                txtOrderID.Clear();
                txtMemberID.Clear();
                txtFreight.Clear();
                dtRequiredDate.CustomFormat = " ";
                dtOrderDate.CustomFormat = " ";
                dtShippedDate.CustomFormat = " ";

                btnDelete.Enabled = false; 
                btnUpdate.Enabled = false;  
                btnNew.Text = "Cancel";
                btnSave.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
                btnNew.Text = "New";
                btnSave.Enabled = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                OrderError errors = new OrderError();
                bool found = false;
                string orderId = txtOrderID.Text;
                string pattern = @"^[0-9.]*$";
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(orderId) == false || orderId.Trim().Equals("") || int.Parse(orderId) < 0)
                {
                    found = true;
                    errors.orderIdError = "Order ID must be the number format and greater than 0!";
                }

                string memberId = txtMemberID.Text;
                if (regex.IsMatch(memberId) == false || memberId.Trim().Equals("") || int.Parse(memberId) < 0)
                {
                    found = true;
                    errors.memberIdError = "Member ID must be the number format and greater than 0!";
                }

                string freight = txtFreight.Text;
                if (!string.IsNullOrEmpty(freight))
                {
                    if (regex.IsMatch(freight) == false || int.Parse(freight) < 0)
                    {
                        found = true;
                        errors.freightError = "Freight must be the number format and greater than 0!";

                    }
                }
                else if (freight.Equals("0"))
                {
                    freight = "0";
                }
                else if (freight == null)
                {
                    freight = "";
                }
                string orderDate = dtOrderDate.Text;
                if (string.IsNullOrEmpty(orderDate))
                {
                    found = true;
                    errors.orderDateError = "Order Date can not be empty";
                }
                string requiredDate = dtRequiredDate.Text;
                if (string.IsNullOrEmpty(requiredDate))
                {
                    requiredDate = " ";
                } 
                string shippedDate = dtShippedDate.Text;
                if (string.IsNullOrEmpty(shippedDate))
                {
                    shippedDate = " ";
                }
                
                if (found)
                {
                    MessageBox.Show($"{errors.orderIdError} \n " +
                        $"{errors.memberIdError} \n " +
                        $"{errors.freightError} \n" +
                        $"{errors.orderDateError} \n" +
                        $"{errors.requiredDateError} \n" +
                        $"{errors.shippedDateError}", "Add a new product - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Order updateOrder = new Order
                    {
                        OrderId = int.Parse(orderId),
                        MemberId = int.Parse(memberId),
                        Freight = !freight.Equals("")?decimal.Parse(freight):null,
                        OrderDate = DateTime.ParseExact(orderDate, "dd/MM/yyyy HH:mm", null),
                        ShippedDate = (!shippedDate.Equals(" "))?DateTime.ParseExact(shippedDate, "dd/MM/yyyy HH:mm", null): null,
                        RequiredDate = (!requiredDate.Equals(" "))?DateTime.ParseExact(requiredDate, "dd/MM/yyyy HH:mm", null): null,
                    };
                    orderRepository.UpdateOrder(updateOrder);
                    LoadOrderList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add a new product - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int orderID = int.Parse(txtOrderID.Text);
            try
            {
                orderRepository.DeleteOrder(orderID);
                LoadOrderList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a order - Error ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void frmOrder_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            LoadOrderList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                OrderError errors = new OrderError();
                bool found = false;
                string orderId = txtOrderID.Text;
                string pattern = @"^[0-9.]*$";
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(orderId) == false || orderId.Trim().Equals("") || int.Parse(orderId) < 0)
                {
                    found = true;
                    errors.orderIdError = "Order ID must be the number format and greater than 0!";
                }

                string memberId = txtMemberID.Text;
                if (regex.IsMatch(memberId) == false || memberId.Trim().Equals("") || int.Parse(memberId) < 0)
                {
                    found = true;
                    errors.memberIdError = "Member ID must be the number format and greater than 0!";
                }

                string freight = txtFreight.Text;
                if (!string.IsNullOrEmpty(freight))
                {
                    if (regex.IsMatch(freight) == false || int.Parse(freight) < 0)
                    {
                        found = true;
                        errors.freightError = "Freight must be the number format and greater than 0!";

                    }
                }
                else if (freight.Equals("0"))
                {
                    freight = "0";
                }
                else if (freight == null)
                {
                    freight = "";
                }
                string orderDate = dtOrderDate.Text;
                if (string.IsNullOrEmpty(orderDate))
                {
                    found = true;
                    errors.orderDateError = "Order Date can not be empty";
                }
                string requiredDate = dtRequiredDate.Text;
                if (string.IsNullOrEmpty(requiredDate))
                {
                    requiredDate = " ";
                }
                string shippedDate = dtShippedDate.Text;
                if (string.IsNullOrEmpty(shippedDate))
                {
                    shippedDate = " ";
                }

                if (found)
                {
                    MessageBox.Show($"{errors.orderIdError} \n " +
                        $"{errors.memberIdError} \n " +
                        $"{errors.freightError} \n" +
                        $"{errors.orderDateError} \n" +
                        $"{errors.requiredDateError} \n" +
                        $"{errors.shippedDateError}", "Add a new product - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Order newOrder = new Order
                    {
                        OrderId = int.Parse(orderId),
                        MemberId = int.Parse(memberId),
                        Freight = !freight.Equals("") ? decimal.Parse(freight) : null,
                        OrderDate = DateTime.ParseExact(orderDate, "dd/MM/yyyy HH:mm", null),
                        ShippedDate = !shippedDate.Equals(" ") ? DateTime.ParseExact(shippedDate, "dd/MM/yyyy HH:mm", null) : null,
                        RequiredDate = !requiredDate.Equals(" ") ? DateTime.ParseExact(requiredDate, "dd/MM/yyyy HH:mm", null) : null,
                    };
                    orderRepository.AddNewOrder(newOrder);
                    LoadOrderList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add a new product - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvOrderList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            txtOrderID.Clear();
            txtMemberID.Clear();
            txtFreight.Clear();
            txtOrderID.Text = dgvOrderList.Rows[index].Cells[0].Value.ToString();
            txtMemberID.Text = dgvOrderList.Rows[index].Cells[1].Value.ToString();
            if (dgvOrderList.Rows[index].Cells[5].Value == null)
            {
                txtFreight.Text = "";
            } else
            {
                txtFreight.Text = dgvOrderList.Rows[index].Cells[5].Value.ToString();
            }
            dtOrderDate.Text = dgvOrderList.Rows[index].Cells[2].Value.ToString();
            if (dgvOrderList.Rows[index].Cells[3].Value == null)
            {
                dtRequiredDate.CustomFormat = " ";
            }else
            {
                dtRequiredDate.CustomFormat = "dd/MM/yyyy HH:mm";
                dtRequiredDate.Text = dgvOrderList.Rows[index].Cells[3].Value.ToString();
            }

            if (dgvOrderList.Rows[index].Cells[4].Value == null)
            {
                dtShippedDate.CustomFormat = " ";
            }
            else
            {
                dtShippedDate.CustomFormat = "dd/MM/yyyy HH:mm";
                dtShippedDate.Text = dgvOrderList.Rows[index].Cells[4].Value.ToString();
            }
        }

        private void dtOrderDate_ValueChanged(object sender, EventArgs e)
        {
            dtOrderDate.CustomFormat = "dd/MM/yyyy HH:mm";
        }

        private void dtRequiredDate_ValueChanged(object sender, EventArgs e)
        {
            dtRequiredDate.CustomFormat = "dd/MM/yyyy HH:mm";
        }

        private void dtShippedDate_ValueChanged(object sender, EventArgs e)
        {
            dtShippedDate.CustomFormat = "dd/MM/yyyy HH:mm";
        }

        private void dtOrderDate_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dtOrderDate.CustomFormat = " ";
            }
        }

        private void dtRequiredDate_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dtRequiredDate.CustomFormat = " ";
            }
        }

        private void dtShippedDate_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dtShippedDate.CustomFormat = " ";
            }
        }
    }
    public record OrderError()
    {
        public string? orderIdError { get; set; }
        public string? memberIdError { get; set; }
        public string? freightError { get; set; }
        public string? orderDateError { get; set; }
        public string? requiredDateError { get; set; }
        public string? shippedDateError { get; set; }
    }
}
