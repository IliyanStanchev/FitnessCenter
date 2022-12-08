using System;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    enum RoleTypes
    {
        Administrator = 1
        , Instructor
        , Cashier
    }
    
    public partial class LoginForm : KryptonForm
    {
        
        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            DatabaseContextWrapper databaseContextWrapper = DatabaseContextWrapper.Instance;

            Employee employee = databaseContextWrapper.Login(usernameTextBox.Text, passwordTextBox.Text);
            if (employee == null)
            {
                MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK);
                return;
            }
            
            RoleTypes roleType = (RoleTypes)employee.RoleId;
            switch (roleType)
            {
                case RoleTypes.Administrator:
                    AdminForm adminForm = new AdminForm();
                    adminForm.InitializeData(employee);
                    adminForm.Show();
                    break;
                case RoleTypes.Instructor:
                    InstructorForm instructorForm = new InstructorForm();
                    instructorForm.InitializeData(employee);
                    instructorForm.Show();
                    break;
                case RoleTypes.Cashier:
                    CashierForm cashierForm = new CashierForm();
                    cashierForm.InitializeData(employee);
                    cashierForm.Show();
                    break;
            }
            
            Hide();
        }

    }
}
