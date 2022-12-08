using ComponentFactory.Krypton.Toolkit;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    public partial class CashierForm : KryptonForm
    {
        private Employee _employee;
        
        public CashierForm()
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

        private void oneTimeTrainingButton_Click(object sender, System.EventArgs e)
        {
            OneTimeTrainingForm oneTimeTrainingForm = new OneTimeTrainingForm();
            oneTimeTrainingForm.InitializeData(_employee);
            oneTimeTrainingForm.Show();
            Hide();
        }

        private void instructorTrainingButton_Click(object sender, System.EventArgs e)
        {
            InstructorTrainingForm instructorTrainingForm = new InstructorTrainingForm();
            instructorTrainingForm.InitializeData(_employee);
            instructorTrainingForm.Show();
            Hide();
        }

        private void registerProductsButton_Click(object sender, System.EventArgs e)
        {
            RegisterProductForm registerProductForm = new RegisterProductForm();
            registerProductForm.InitializeData(_employee);
            registerProductForm.Show();
            Hide();
        }

        private void newSubscriptionButton_Click(object sender, System.EventArgs e)
        {
            NewSubscriptionForm newSubscriptionForm = new NewSubscriptionForm();
            newSubscriptionForm.InitializeData(_employee);
            newSubscriptionForm.Show();
            Hide();
        }

        private void checkInCustomerButton_Click(object sender, System.EventArgs e)
        {
            CheckInCustomerForm checkInCustomerForm = new CheckInCustomerForm();
            checkInCustomerForm.InitializeData(_employee);
            checkInCustomerForm.Show();
            Hide();
        }

        private void shopButton_Click(object sender, System.EventArgs e)
        {
            ShopForm shopForm = new ShopForm();
            shopForm.InitializeData(_employee);
            shopForm.Show();
            Hide();
        }
    }
}
