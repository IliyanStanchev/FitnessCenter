using System;
using System.CodeDom;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    enum TrainingTypes
    {
        [Description("Instructor")]
        Customer,

        [Description("Group")]
        Group
    }
    
    public partial class InstructorTrainingForm : KryptonForm
    {
        private static readonly double InstructorPrice = 10;
        
        private Employee _employee;
        private Customer _customer;

        public InstructorTrainingForm()
        {
            InitializeComponent();

            instructorsComboBox.DataSource = DatabaseContextWrapper.Instance.Context.EMPLOYEES
                .Where(e => e.RoleId == (int)RoleTypes.Instructor)
                .ToList();

            dateDatePicker.Value = DateTime.Now;
            hourNumericUpDown.Minimum = 7;
            hourNumericUpDown.Maximum = 22;
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
                        FirstName = firstNameEditBox.Text,
                        LastName = lastNameEditBox.Text,
                        RegistrationDate = DateTime.Now,
                        Email = emailEditBox.Text,
                        PhoneNumber = phoneNumberEditBox.Text,
                    };

                    _customer = instance.CUSTOMERS.Add(_customer);
                }

                InstructorSchedule instructorSchedule = new InstructorSchedule
                {
                    CustomerId = _customer.Id,
                    EmployeeId = ((Employee)instructorsComboBox.SelectedItem).Id,
                    TrainingType = (int)TrainingTypes.Customer,
                    StartDate = dateDatePicker.Value.Date.AddHours((double)hourNumericUpDown.Value),
                    EndDate = dateDatePicker.Value.Date.AddHours((double)hourNumericUpDown.Value).AddHours(1),
                };

                Balance balance = new Balance
                {
                    Date = DateTime.Now,
                    OperationType = (int)OperationTypes.Income,
                    Value = InstructorPrice,
                    Description = "Instructor training for " + _customer.FirstName + " " + _customer.LastName + " with " + instructorsComboBox.SelectedItem,
                };

                instance.INSTRUCTOR_SCHEDULES.Add(instructorSchedule);
                instance.BALANCES.Add(balance);
                instance.SaveChanges();

                MessageBox.Show("Instructor training saved successfully.", "", MessageBoxButtons.OK);
                GoBack();
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
            catch (Exception exception)
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
    }
}
