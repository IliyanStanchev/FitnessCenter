using System.Linq;
using FitnessCenter.Models;

namespace FitnessCenter.Views
{
    public sealed class DatabaseContextWrapper
    {
        private static readonly object Padlock = new object();
        
        public FitnessContext Context { get; set; }

        DatabaseContextWrapper()
        {
            Context = new FitnessContext();
        }

        public static DatabaseContextWrapper Instance
        {
            get
            {
                lock (Padlock)
                {
                    return new DatabaseContextWrapper();
                }
            }
        }

        public Employee Login(string username, string password)
        {
            return Context.EMPLOYEES.FirstOrDefault(e => e.Username == username && e.Password == password);
        }
    }
}
