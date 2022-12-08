using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    public partial class CheckInCustomerForm : KryptonForm
    {

        private Employee _employee;
        
        public CheckInCustomerForm()
        {
            InitializeComponent();

            customerNameTextBox.Enabled = false;
            validUntilTextBox.Enabled = false;
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
            using (var instance = DatabaseContextWrapper.Instance.Context)
            {
                if (!ValidateControls())
                    return;

                if (!ValidateData())
                    return;
                
                CustomerVisit customerVisit = new CustomerVisit
                {
                    Date = DateTime.Now,
                    VisitType = (int)VisitTypes.Subscription,
                    CustomerName = customerNameTextBox.Text,
                };

                instance.CUSTOMER_VISITS.Add(customerVisit);
                instance.SaveChanges();

                MessageBox.Show("Single training registered successfully.", "", MessageBoxButtons.OK);
                GoBack();
            }
        }

        private bool ValidateData()
        {
            int subscriptionId = Convert.ToInt32(subscriptionCodeTextBox.Text);
            CustomerSubscriptions subscription = DatabaseContextWrapper.Instance.Context.CUSTOMER_SUBSCRIPTIONS.FirstOrDefault(s => s.Id == subscriptionId);

            if (subscription == null)
            {
                MessageBox.Show("Subscription with this code does not exist.", "", MessageBoxButtons.OK);
                return false;
            }

            if (subscription.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("Subscription with has expired.", "", MessageBoxButtons.OK);
                return false;
            }
            
            return true;
        }

        private bool ValidateControls()
        {
            if (!string.IsNullOrEmpty(subscriptionCodeTextBox.Text)) return true;
            
            MessageBox.Show("Enter subscription code.", "", MessageBoxButtons.OK);
            return false;
        }

        private void subscriptionCodeTextBox_Leave(object sender, EventArgs e)
        {
            int subscriptionId = Convert.ToInt32(subscriptionCodeTextBox.Text);
            CustomerSubscriptions subscription = DatabaseContextWrapper.Instance.Context.CUSTOMER_SUBSCRIPTIONS.FirstOrDefault(s => s.Id == subscriptionId);

            if (subscription == null)
            {
                MessageBox.Show("Subscription with this code does not exist.", "", MessageBoxButtons.OK);
                return;
            }

            customerNameTextBox.Text = subscription.Customer.FirstName + " " + subscription.Customer.LastName;
            validUntilTextBox.Text = subscription.ExpirationDate.ToString();
        }
    }
}
