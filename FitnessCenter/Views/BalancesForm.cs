using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    enum OperationTypes
    {
        Income
        , Expense
    }
    
    public partial class BalancesForm : KryptonForm
    {
        private Employee _employee;

        public BalancesForm()
        {
            InitializeComponent();

            employeesGridView.AutoGenerateColumns = false;
            
            monthNumericUpDown.Maximum = 12;
            monthNumericUpDown.Minimum = 1;

            yearNumericUpDown.Maximum = 9999;
            yearNumericUpDown.Minimum = 2000;
            
            monthNumericUpDown.Value = DateTime.Now.Month;
            yearNumericUpDown.Value = DateTime.Now.Year;

            UpdateBalances();
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
            UpdateBalances();
        }

        private void yearNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateBalances();
        }

        private void UpdateBalances()
        {
            employeesGridView.Rows.Clear();

            int month = (int)monthNumericUpDown.Value;
            int year = (int)yearNumericUpDown.Value;

            List<Balance> balances = DatabaseContextWrapper.Instance.Context.BALANCES.ToList();

            foreach (var balance in balances)
            {
                if (balance.Date.Month == month && balance.Date.Year == year)
                {
                    employeesGridView.Rows.Add(balance.Date, balance.OperationType == (int)OperationTypes.Income ? "Income" : "Expense", balance.Value, balance.Description);
                }
            }
        }
    }
}
