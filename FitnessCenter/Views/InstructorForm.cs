using System;
using System.Linq;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    public partial class InstructorForm : KryptonForm
    {
        private Employee _employee;
        
        public InstructorForm()
        {
            InitializeComponent();
        }

        public void InitializeData(Employee employee)
        {
            _employee = employee;
            welcomeLabel.Text = "Welcome, " + _employee.FirstName + " " + _employee.LastName;

            scheduleDateTimePicker.Value = DateTime.Now;
            UpdateSchedule();
        }

        private void UpdateSchedule()
        {
            scheduleGridView.Rows.Clear();

            using (var context = DatabaseContextWrapper.Instance.Context)
            {
                var schedules = context.INSTRUCTOR_SCHEDULES.OrderBy(s => s.StartDate).ToList();

                foreach (var item in schedules)
                {
                    if (item.StartDate.Year != scheduleDateTimePicker.Value.Year ||
                        item.StartDate.Month != scheduleDateTimePicker.Value.Month ||
                        item.StartDate.Day != scheduleDateTimePicker.Value.Day)
                        continue;
                   
                    Customer customer = context.CUSTOMERS.Find(item.CustomerId);
                    string customerName = customer == null ? "" : customer.FirstName + " " + customer.LastName;
                    scheduleGridView.Rows.Add(customerName, item.StartDate, item.EndDate);
                }
            }
        }

        private void loginButton_Click(object sender, System.EventArgs e)
        {
            new LoginForm().Show();
            Hide();
        }

       

        private void scheduleDateTimePicker_ValueChanged(object sender, System.EventArgs e)
        {
            UpdateSchedule();
        }
    }
}
