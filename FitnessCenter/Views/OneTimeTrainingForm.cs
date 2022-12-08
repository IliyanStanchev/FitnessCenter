using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;
using Guna.UI2.WinForms.Suite;

namespace FitnessCenter.Views
{
    public partial class OneTimeTrainingForm : KryptonForm
    {
        enum CustomerTypes
        {
            [Description("Student")]
            Student = 5,
            
            [Description("Employee")]
            Employee = 3,

            [Description("Normal")]
            Normal = 7
        }

        private Employee _employee;
        
        public OneTimeTrainingForm()
        {
            InitializeComponent();

            customerTypeComboBox.DataSource = Enum.GetValues(typeof(CustomerTypes));
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

        private void registerEmployeeButton_Click(object sender, EventArgs e)
        {
            using (var instance = DatabaseContextWrapper.Instance.Context)
            {
                if (!ValidateControls())
                    return;
                
                CustomerVisit customerVisit = new CustomerVisit
                {
                    Date = DateTime.Now,
                    VisitType = (int)VisitTypes.SingleTime,
                    CustomerName = customerNameTextBox.Text,
                };

                Balance balance = new Balance
                {
                    Date = DateTime.Now,
                    OperationType = (int)OperationTypes.Income,
                    Value = (int)customerTypeComboBox.SelectedValue,
                    Description = "One time training: " + customerNameTextBox.Text
                };
                    
                instance.CUSTOMER_VISITS.Add(customerVisit);
                instance.BALANCES.Add(balance);
                instance.SaveChanges();

                MessageBox.Show("Single training registered successfully.", "", MessageBoxButtons.OK);
                GoBack();
            }
        }

        private bool ValidateControls()
        {
            if (!string.IsNullOrEmpty(customerNameTextBox.Text)) return true;
            
            MessageBox.Show("Customer name cannot be empty.", "Register Employee Error", MessageBoxButtons.OK);
            return false;
        }
    }
}
