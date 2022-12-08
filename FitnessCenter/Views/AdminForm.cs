using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    public partial class AdminForm : KryptonForm
    {
        private Employee _employee;
        
        public AdminForm()
        {
            InitializeComponent();
        }

        public void InitializeData(Employee employee)
        {
            _employee = employee;
            welcomeLabel.Text = "Welcome, " + _employee.FirstName + " " + _employee.LastName;
        }

        private void loginButton_Click(object sender, System.EventArgs e)
        {
            new LoginForm().Show();
            Hide();
        }

        private void registerEmployeeButton_Click(object sender, System.EventArgs e)
        {
            RegisterEmployeeForm registerEmployeeForm = new RegisterEmployeeForm();
            registerEmployeeForm.InitializeData(_employee);
            registerEmployeeForm.Show();
            Hide();
        }

        private void employeesButton_Click(object sender, System.EventArgs e)
        {
            EmployeesForm employeesForm = new EmployeesForm();
            employeesForm.InitializeData(_employee);
            employeesForm.Show();
            Hide();
        }

        private void employeeSalaryButton_Click(object sender, System.EventArgs e)
        {
            EmployeesSalaryForm employeesSalaryForm = new EmployeesSalaryForm();
            employeesSalaryForm.InitializeData(_employee);
            employeesSalaryForm.Show();
            Hide();
        }

        private void monthlyReportButton_Click(object sender, System.EventArgs e)
        {
            BalancesForm balancesForm = new BalancesForm();
            balancesForm.InitializeData(_employee);
            balancesForm.Show();
            Hide();
        }

        private void visitsButton_Click(object sender, System.EventArgs e)
        {
            CustomerVisitsForm customerVisitsForm = new CustomerVisitsForm();
            customerVisitsForm.InitializeData(_employee);
            customerVisitsForm.Show();
            Hide();
        }

        private void customerVisitsDiagramButton_Click(object sender, System.EventArgs e)
        {
            CustomerVisitsDiagramForm customerVisitsDiagramForm = new CustomerVisitsDiagramForm();
            customerVisitsDiagramForm.InitializeData(_employee);
            customerVisitsDiagramForm.Show();
            Hide();
        }
    }
}
