using System;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    public partial class ShopForm : KryptonForm
    {

        private Employee _employee;
        
        public ShopForm()
        {
            InitializeComponent();

            productsComboBox.DataSource = DatabaseContextWrapper.Instance.Context.PRODUCTS.Where(x => x.Amount > 0).ToList();
            priceTextBox.Enabled = false;
        }

        public void InitializeData(Employee employee)
        {
            _employee = employee;
        }

        private void GoBack()
        {
            CashierForm cashierForm = new CashierForm();
            cashierForm.InitializeData(_employee);
            cashierForm.Show();
            Hide();
        }

        private void goBackButton_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            using ( var instance = DatabaseContextWrapper.Instance.Context)
            {
                if (!ValidateControls())
                    return;

                Product product = productsComboBox.SelectedItem as Product;
                if (product == null)
                    return;

                product.Amount -= (int)quantityNumericUpDown.Value;

                Balance balance = new Balance
                {
                    Date = DateTime.Now,
                    Value = product.Price,
                    OperationType = (int)OperationTypes.Income,
                    Description = "Product sold: " + product.Name
                };

                instance.PRODUCTS.AddOrUpdate(product);
                instance.BALANCES.Add(balance);
                instance.SaveChanges();

                MessageBox.Show("Product sold successfully.", "", MessageBoxButtons.OK);
                GoBack();
            }
        }

        private bool ValidateControls()
        {
            Product product = (Product)productsComboBox.SelectedItem;
            if (product == null)
            {
                MessageBox.Show("Please select a product.", "", MessageBoxButtons.OK);
                return false;
            }

            if (product.Amount < quantityNumericUpDown.Value)
            {
                MessageBox.Show("There are not enoght products. Current availability: " + product.Amount, "", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void productsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product product = (Product)productsComboBox.SelectedItem;
            priceTextBox.Text = product.Price.ToString(CultureInfo.InvariantCulture);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Product product = (Product)productsComboBox.SelectedItem;
            if (product == null)
                return;

            totalLabel.Text = "Total amount:" + (product.Price * (int)quantityNumericUpDown.Value).ToString(CultureInfo.InvariantCulture);
        }
    }
}
