using System;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    public partial class RegisterEmployeeForm : KryptonForm
    {
        private Employee _employee;
        
        public RegisterEmployeeForm()
        {
            InitializeComponent();
            
            using (DatabaseContextWrapper.Instance.Context)
            {
                positionComboBox.DataSource = DatabaseContextWrapper.Instance.Context.ROLES.ToList();
            }
        }

        public void InitializeData(Employee employee)
        {
            _employee = employee;
        }

        private void GoBack()
        {
            AdminForm adminForm = new AdminForm();
            adminForm.InitializeData(_employee);
            adminForm.Show();
            Hide();
        }

        private void goBackButton_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void registerEmployeeButton_Click(object sender, EventArgs e)
        {
            using ( var instance = DatabaseContextWrapper.Instance.Context)
            {
                if (!ValidateControls())
                    return;
                
                Employee employee = new Employee
                {
                    Username = usernameTextBox.Text,
                    Password = passwordTextBox.Text,
                    FirstName = firstNameTextBox.Text,
                    LastName = lastNameTextBox.Text,
                    AppointmentDate = DateTime.Now,
                    Salary = double.Parse(salaryTextBox.Text),
                    RoleId = instance.ROLES.First(role => role.Name == positionComboBox.Text).Id
                };

                if (!ValidateData(employee))
                    return;

                instance.EMPLOYEES.Add(employee);
                instance.SaveChanges();

                MessageBox.Show("Employee registered successfully.", "Register Employee", MessageBoxButtons.OK);
                GoBack();
            }
        }

        private bool ValidateControls()
        {
            if (string.IsNullOrEmpty(usernameTextBox.Text))
            {
                MessageBox.Show("Username cannot be empty.", "Register Employee Error", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Password cannot be empty.", "Register Employee Error", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(firstNameTextBox.Text))
            {
                MessageBox.Show("First name cannot be empty.", "Register Employee Error", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(lastNameTextBox.Text))
            {
                MessageBox.Show("Last name cannot be empty.", "Register Employee Error", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(salaryTextBox.Text))
            {
                MessageBox.Show("Salary cannot be empty.", "Register Employee Error", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(positionComboBox.Text))
            {
                MessageBox.Show("Position cannot be empty.", "Register Employee Error", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private bool ValidateData(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Username))
            {
                MessageBox.Show("Username cannot be empty.", "Register Employee Error", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(employee.Password))
            {
                MessageBox.Show("Password cannot be empty.", "Register Employee Error", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(employee.FirstName))
            {
                MessageBox.Show("First name cannot be empty.", "Register Employee Error", MessageBoxButtons.OK);
                return false;
            }

            if (string.IsNullOrEmpty(employee.LastName))
            {
                MessageBox.Show("Last name cannot be empty.", "Register Employee Error", MessageBoxButtons.OK);
                return false;
            }

            if (employee.Salary == 0)
            {
                MessageBox.Show("Salary cannot be empty.", "Register Employee Error", MessageBoxButtons.OK);
                return false;
            }

            if (DatabaseContextWrapper.Instance.Context.EMPLOYEES.Any(emp => emp.Username == employee.Username))
            {
                MessageBox.Show("Username already exists.", "Register Employee Error", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private void salaryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
