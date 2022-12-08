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
    public partial class RegisterProductForm : KryptonForm
    {
        enum ProductCategories
        {
            [Description("Protein Powder")] Protein,

            [Description("Essential Amino Acids")] Eaa,

            [Description("BCAA")] Bcaa,

            [Description("Pre - Workout")] PreWorkout,

            [Description("Glutamine")] Glutamine,

            [Description("Creatine")] Creatine,

            [Description("Protein foods")] ProteinFoods,

        }

        private Employee _employee;
        private Product _product;

        public RegisterProductForm()
        {
            InitializeComponent();
            providerComboBox.DataSource = DatabaseContextWrapper.Instance.Context.PROVIDERS.ToList();
            categoryComboBox.DataSource = Enum.GetValues(typeof(ProductCategories));

            deliveryDateDatePicker.Value = DateTime.Now;
            nextDeliveryDatePicker.Value = DateTime.Now;
            nextDeliveryDatePicker.Enabled = false;

            pricePerUnitTextBox.Text = "0";
        }

        public void InitializeData(Employee employee)
        {
            _employee = employee;
            _product = null;
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

        private void registerProductButton_Click(object sender, EventArgs e)
        {
            using (var instance = DatabaseContextWrapper.Instance.Context)
            {
                if (!ValidateControls())
                    return;

                if (_product == null)
                {
                    if (!ValidateData())
                        return;

                    _product = new Product()
                    {
                        Name = productNameTextBox.Text,
                        Amount = (int)amountNumericUpDown.Value,
                        Barcode = barcodeTextBox.Text,
                        Price = double.Parse(priceTextBox.Text, CultureInfo.InvariantCulture),
                        Category = (int)categoryComboBox.SelectedItem,
                        LastDeliveryDate = DateTime.Now,
                        NextDeliveryDate = GetNextDeliveryDate(),
                    };

                    _product = instance.PRODUCTS.Add(_product);
                }
                else
                {
                    _product.Amount += (int)amountNumericUpDown.Value;
                    instance.PRODUCTS.AddOrUpdate(_product);
                }

                Balance balance = new Balance
                {
                    Date = DateTime.Now,
                    OperationType = (int)OperationTypes.Expense,
                    Value = (int)amountNumericUpDown.Value * Double.Parse(pricePerUnitTextBox.Text),
                    Description = "Bought products " + _product.Name + " Amount: " + (int)amountNumericUpDown.Value +
                                  " From: " + providerComboBox.SelectedItem,
                };

                instance.BALANCES.Add(balance);
                instance.SaveChanges();

                MessageBox.Show("Products bought successfully.", "", MessageBoxButtons.OK);
                GoBack();
            }
        }

        private DateTime GetNextDeliveryDate()
        {
            switch ((ProductCategories)categoryComboBox.SelectedIndex)
            {
                case ProductCategories.Protein:
                case ProductCategories.Eaa:
                case ProductCategories.Bcaa:
                case ProductCategories.PreWorkout:
                case ProductCategories.Glutamine:
                case ProductCategories.Creatine:
                    return deliveryDateDatePicker.Value.AddMonths(1);
                case ProductCategories.ProteinFoods:
                    return deliveryDateDatePicker.Value.AddDays(7);

                default:
                    return DateTime.Now;
            }
        }

        private bool ValidateData()
        {
            return true;
        }

        private bool ValidateControls()
        {
            if (string.IsNullOrEmpty(productNameTextBox.Text))
            {
                MessageBox.Show("Please enter product name.", "", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(barcodeTextBox.Text))
            {
                MessageBox.Show("Please enter barcode.", "", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(priceTextBox.Text))
            {
                MessageBox.Show("Please enter price.", "", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void productNameTextBox_Leave(object sender, EventArgs e)
        {
            _product = DatabaseContextWrapper.Instance.Context.PRODUCTS.FirstOrDefault(p =>
                p.Name == productNameTextBox.Text);
            if (_product == null)
                return;

            barcodeTextBox.Text = _product.Barcode;
            priceTextBox.Text = _product.Price.ToString(CultureInfo.InvariantCulture);
            categoryComboBox.SelectedIndex = _product.Category;

            barcodeTextBox.Enabled = false;
            priceTextBox.Enabled = false;
            categoryComboBox.Enabled = false;
        }

        private void amountNumericUpDown_ValueChanged(object sender, EventArgs e)
        { 
            priceLabel.Text = "Price to be paid:" + (int)amountNumericUpDown.Value * Double.Parse( pricePerUnitTextBox.Text );
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            priceLabel.Text = "Price to be paid:" + (int)amountNumericUpDown.Value * Double.Parse(pricePerUnitTextBox.Text);
        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            nextDeliveryDatePicker.Value = GetNextDeliveryDate();
        }
    }
}
