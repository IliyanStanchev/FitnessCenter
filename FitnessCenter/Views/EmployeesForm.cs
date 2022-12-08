using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    public partial class EmployeesForm : KryptonForm
    {
        private Employee _employee;
        
        public EmployeesForm()
        {
            InitializeComponent();

            employeesGridView.AutoGenerateColumns = false;
            
            List<Employee> employees = DatabaseContextWrapper.Instance.Context.EMPLOYEES.ToList();

            foreach (var employee in employees)
            {
                employeesGridView.Rows.Add(employee.Username, employee.FirstName, employee.LastName, employee.Role.Name, employee.Salary);
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
    }
}
