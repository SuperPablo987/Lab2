using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    public class CustomerManager
    {
        /// <summary>
        /// customer is authenticated based on username and password and customer is returned if valid or null if not
        /// </summary>
        /// <param name="username">provided username</param>
        /// <param name="password">provided password</param>
        /// <returns></returns>
        public static Customer? Authenticate(string username, string password)
        {
            Customer? customer= null;
            using (InlandMarinaContext dB = new InlandMarinaContext())
            {
                customer = dB.Customers.SingleOrDefault(usr => usr.Username == username
                                                    && usr.Password == password); 
            }
            
            return customer; //this will either be null or an object
        }
        /// <summary>
        /// adds new customer to the database
        /// </summary>
        /// <param name="customer">customer data to add</param>
        public static void Add(Customer customer)
        {
            using (InlandMarinaContext dB = new InlandMarinaContext())
            {
                dB.Customers.Add(customer);
                dB.SaveChanges();
            }
        }
    }
}
