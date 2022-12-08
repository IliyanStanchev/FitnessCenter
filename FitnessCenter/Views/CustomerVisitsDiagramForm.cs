using System;
using System.Collections.Generic;
using System.Linq;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    public partial class CustomerVisitsDiagramForm : KryptonForm
    {
        private Employee _employee;

        public CustomerVisitsDiagramForm()
        {
            InitializeComponent();

            monthNumericUpDown.Maximum = 12;
            monthNumericUpDown.Minimum = 1;

            yearNumericUpDown.Maximum = 9999;
            yearNumericUpDown.Minimum = 2000;
            
            monthNumericUpDown.Value = DateTime.Now.Month;
            yearNumericUpDown.Value = DateTime.Now.Year;

            UpdateCustomerChart();
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
            UpdateCustomerChart();
        }

        private void yearNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateCustomerChart();
        }

        private void UpdateCustomerChart()
        {
            customerVisitsChart.Series[0].Points.Clear();

            int month = (int)monthNumericUpDown.Value;
            int year = (int)yearNumericUpDown.Value;
            
            List<CustomerVisit> visits = DatabaseContextWrapper.Instance.Context.CUSTOMER_VISITS.ToList();
            visits.Sort((a, b) => String.Compare(a.CustomerName, b.CustomerName, StringComparison.Ordinal));

            string currentCustomer = null;
            foreach (var visit in visits)
            {
                if ( string.IsNullOrEmpty(currentCustomer) || currentCustomer != visit.CustomerName )
                {
                    currentCustomer = visit.CustomerName;
                    var visitsInMonth = 0;
                    try
                    {
                        visitsInMonth = visits.Where(v => v.CustomerName == currentCustomer && v.Date.Month == month && v.Date.Year == year).ToList().Count;
                    }
                    catch (Exception)
                    {
                        visitsInMonth = 0;
                    }

                    
                    if( visitsInMonth > 0 )
                        customerVisitsChart.Series[0].Points.AddXY(currentCustomer, visitsInMonth);
                }
            }
        }
    }
}
