using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    /// <summary>
    /// repository of all Leases and access with database
    /// </summary>
    public class LeaseManager
    {
        /// <summary>
        /// return as list of all leases
        /// </summary>
        /// <returns>list of leases or null if none</returns>
        public static List<Lease> GetLeases()
        {
            List<Lease> leases = null;
            using (InlandMarinaContext dB = new InlandMarinaContext())
            {
                leases = dB.Leases.ToList();
            }
            return leases;
        }

        /// <summary>
        /// return a list of leases by given customer id
        /// </summary>
        /// <param name="id">id for given customer</param>
        /// <returns>list of all leases or none if null</returns>
        public static List<Lease> GetLeaseByCustomer(int id)
        {
            List<Lease> leases = null;
            using (InlandMarinaContext dB = new InlandMarinaContext())
            {
                leases = dB.Leases.Where(l => l.CustomerID == id).ToList();
            }
            return leases;
        }
        /// <summary>
        /// gets a list of leases by given slip id
        /// </summary>
        /// <param name="slipId">given slip id</param>
        /// <returns>list of slip id or null if none</returns>
        public static List<Lease> GetLeaseById(int slipId)
        {
            List<Lease> leases = null;
            using (InlandMarinaContext dB = new InlandMarinaContext())
            {
                leases = dB.Leases.Where(l => l.SlipID == slipId).ToList();
            }
            return leases;
        }
    }
}
