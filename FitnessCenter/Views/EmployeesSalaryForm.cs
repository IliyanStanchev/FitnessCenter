using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    public partial class EmployeesSalaryForm : KryptonForm
    {
        private Employee _employee;
        private static readonly double Tolerance = 0.05;

        public EmployeesSalaryForm()
        {
            InitializeComponent();

            highestSalaryGridView.AutoGenerateColumns = false;
            
            List<Employee> employees = DatabaseContextWrapper.Instance.Context.EMPLOYEES.ToList();

            double highestSalary = 0;
            double lowestSalary = Double.MaxValue;

            foreach (var employee in employees)
            {
                try
                {
                    var averageSalary = employee.SALARIES.Where(salary => salary.Date.Year > DateTime.Now.Year - 5).Where(salary => salary.EmployeeId == employee.Id).Average(salary => salary.Value);
                    employee.Salary = averageSalary;
                }
                catch (Exception)
                {
                    employee.Salary = 0;
                }

                if( employee.Salary == 0 )
                    continue;

                if (employee.Salary > highestSalary)
                {
                    highestSalary = employee.Salary;
                }

                if (employee.Salary < lowestSalary)
                {
                    lowestSalary = employee.Salary;
                }
            }

            foreach (var employee in employees)
            {
                if (employee.Salary == 0)
                    continue;
                
                if (Math.Abs(employee.Salary - highestSalary) < Tolerance)
                {
                    highestSalaryGridView.Rows.Add(employee.Username, employee.FirstName, employee.LastName, employee.Role.Name, employee.Salary);
                }
                else if (Math.Abs(employee.Salary - lowestSalary) < Tolerance)
                {
                    lowestSalaryGridView.Rows.Add(employee.Username, employee.FirstName, employee.LastName, employee.Role.Name, employee.Salary);
                }
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
