using System;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    public partial class NewSubscriptionForm : KryptonForm
    {
        enum SubscriptionTypes
        {
            [Description("Student One Week")]
            StudentWeekly,

            [Description("Student One Month")]
            StudentMonthly,
            
            [Description("Student One Year")]
            StudentYearly,
            
            [Description("Employee One Week")]
            EmployeeWeekly,

            [Description("Employee One Month")]
            EmployeeMonthly,

            [Description("Employee One Year")]
            EmployeeYearly,

            [Description("Normal One Week")]
            NormalWeekly,

            [Description("Normal One Month")]
            NormalMonthly,

            [Description("Normal One Year")]
            NormalYearly,
        }

        private Employee _employee;
        private Customer _customer;

        public NewSubscriptionForm()
        {
            InitializeComponent();
            subscriptionComboBox.DataSource = Enum.GetValues(typeof(SubscriptionTypes));

            beginDateDatePicker.Value = DateTime.Now;
            endDateDatePicker.Value = DateTime.Now;
            endDateDatePicker.Enabled = false;
        }

        public void InitializeData(Employee employee)
        {
            _employee = employee;
            _customer = null;
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

                if (_customer == null)
                {
                    if (!ValidateData())
                        return;
                    
                    _customer = new Customer
                    {
                        Id = 0,
                        FirstName = firstNameEditBox.Text,
                        LastName = lastNameEditBox.Text,
                        RegistrationDate = DateTime.Now,
                        Email = emailEditBox.Text,
                        PhoneNumber = phoneNumberEditBox.Text,
                    };

                    _customer = instance.CUSTOMERS.Add(_customer);
                }

                CustomerSubscriptions customerSubscriptions = new CustomerSubscriptions
                {
                    CustomerId = _customer.Id,
                    CreationDate = beginDateDatePicker.Value,
                    ExpirationDate = GetExpirationDate(),
                    Type = (int)subscriptionComboBox.SelectedItem,
                };

                Balance balance = new Balance
                {
                    Date = DateTime.Now,
                    OperationType = (int)OperationTypes.Income,
                    Value = GetPrice(),
                    Description = "New subscription for " + _customer.FirstName + " " + _customer.LastName,
                };

                instance.CUSTOMER_SUBSCRIPTIONS.Add(customerSubscriptions);
                instance.BALANCES.Add(balance);
                instance.SaveChanges();

                MessageBox.Show("Subscription saved successfully.", "", MessageBoxButtons.OK);
                GoBack();
            }
        }

        private double GetPrice()
        {
            switch ((SubscriptionTypes)subscriptionComboBox.SelectedItem)
            {
                case SubscriptionTypes.StudentWeekly:
                    return 10;
                case SubscriptionTypes.StudentMonthly:
                    return 30;
                case SubscriptionTypes.StudentYearly:
                    return 300;
                case SubscriptionTypes.EmployeeWeekly:
                    return 20;
                case SubscriptionTypes.EmployeeMonthly:
                    return 60;
                case SubscriptionTypes.EmployeeYearly:
                    return 600;
                case SubscriptionTypes.NormalWeekly:
                    return 40;
                case SubscriptionTypes.NormalMonthly:
                    return 120;
                case SubscriptionTypes.NormalYearly:
                    return 1200;
                default:
                    return 0;
            }
        }

        private DateTime GetExpirationDate()
        {
            switch ((SubscriptionTypes)subscriptionComboBox.SelectedIndex)
            {
                case SubscriptionTypes.StudentWeekly:
                case SubscriptionTypes.EmployeeWeekly:
                case SubscriptionTypes.NormalWeekly:
                    return beginDateDatePicker.Value.AddDays(7);
                
                case SubscriptionTypes.StudentMonthly:
                case SubscriptionTypes.EmployeeMonthly:
                case SubscriptionTypes.NormalMonthly:
                    return beginDateDatePicker.Value.AddMonths(1);

                case SubscriptionTypes.StudentYearly:
                case SubscriptionTypes.EmployeeYearly:
                case SubscriptionTypes.NormalYearly:
                    return beginDateDatePicker.Value.AddYears(1);
                default:
                    return DateTime.Now;
            }
        }

        private bool ValidateData()
        {
            if (DatabaseContextWrapper.Instance.Context.CUSTOMERS.Any(c => c.Email == emailEditBox.Text))
            {
                MessageBox.Show("Customer with this email already exists.", "", MessageBoxButtons.OK);
                return false;
            }

            if (DatabaseContextWrapper.Instance.Context.CUSTOMERS.Any(c => c.PhoneNumber == phoneNumberEditBox.Text))
            {
                MessageBox.Show("Customer with this phone number already exists.", "", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private bool ValidateControls()
        {
            if (string.IsNullOrEmpty(firstNameEditBox.Text))
            {
                MessageBox.Show("Please enter first name.", "", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(lastNameEditBox.Text))
            {
                MessageBox.Show("Please enter last name.", "", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(phoneNumberEditBox.Text))
            {
                MessageBox.Show("Please enter phone number.", "", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(emailEditBox.Text))
            {
                MessageBox.Show("Please enter email.", "", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void customerCodeEditBox_Leave(object sender, EventArgs e)
        {

            int lCustomerId = 0;

            try
            {
                lCustomerId = Convert.ToInt32(customerCodeEditBox.Text);
            }
            catch (Exception)
            {
                return;
            }
            
            _customer = DatabaseContextWrapper.Instance.Context.CUSTOMERS.FirstOrDefault(c => c.Id == lCustomerId);
            if (_customer != null)
            {
                firstNameEditBox.Text = _customer.FirstName;
                lastNameEditBox.Text = _customer.LastName;
                emailEditBox.Text = _customer.Email;
                phoneNumberEditBox.Text = _customer.PhoneNumber;

                firstNameEditBox.Enabled = false;
                lastNameEditBox.Enabled = false;
                emailEditBox.Enabled = false;
                phoneNumberEditBox.Enabled = false;
            }
        }

        private void subscriptionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            priceLabel.Text = "Price " + GetPrice();
            endDateDatePicker.Value = GetExpirationDate();
        }
    }
}
