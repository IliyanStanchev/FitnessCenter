using System;
using System.Collections.Generic;
using System.Linq;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    enum VisitTypes
    {
        SingleTime
        , Subscription
    }
    
    public partial class CustomerVisitsForm : KryptonForm
    {
        
        private Employee _employee;
        private bool _isDailyReport;

        public CustomerVisitsForm()
        {
            InitializeComponent();

            employeesGridView.AutoGenerateColumns = false;

            dayNumericUpDown.Maximum = 31;
            dayNumericUpDown.Minimum = 1;
            
            monthNumericUpDown.Maximum = 12;
            monthNumericUpDown.Minimum = 1;

            yearNumericUpDown.Maximum = 9999;
            yearNumericUpDown.Minimum = 2000;

            dayNumericUpDown.Value = DateTime.Now.Day;
            monthNumericUpDown.Value = DateTime.Now.Month;
            yearNumericUpDown.Value = DateTime.Now.Year;

            _isDailyReport = true;
            dailyReportCheckBox.Checked = _isDailyReport;

            UpdateCustomerVisits();
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

        private void monthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateCustomerVisits();
        }

        private void yearNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateCustomerVisits();
        }

        private void dayNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateCustomerVisits();
        }

        private void dailyReportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _isDailyReport = dailyReportCheckBox.Checked;
            dayNumericUpDown.Enabled = _isDailyReport;

            UpdateCustomerVisits();
        }

        private void UpdateCustomerVisits()
        {
            employeesGridView.Rows.Clear();

            int day = (int)dayNumericUpDown.Value;
            int month = (int)monthNumericUpDown.Value;
            int year = (int)yearNumericUpDown.Value;

            List<CustomerVisit> customerVisits = DatabaseContextWrapper.Instance.Context.CUSTOMER_VISITS.ToList();

            int totalVisits = 0;
            foreach (var visit in customerVisits)
            {
                if (_isDailyReport && visit.Date.Day != day)
                    continue;
                
                if (visit.Date.Month == month && visit.Date.Year == year)
                {
                    employeesGridView.Rows.Add(visit.Date, visit.VisitType == (int)VisitTypes.SingleTime ? "Single training" : "Subscription", visit.CustomerName);
                    totalVisits++;
                }
            }

            totalVisitsLabel.Text = "Total visits:" + totalVisits;
        }
    }
}
