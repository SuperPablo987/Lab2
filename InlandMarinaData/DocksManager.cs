using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    /// <summary>
    /// repository of all methods concerning docs accessing the database
    /// </summary>
    public class DocksManager
    {
        /// <summary>
        /// get a list of all docks
        /// </summary>
        /// <returns>return a list of docks or null if none</returns>
        public static List<Dock> GetDocks()
        {
            List<Dock> docks = null;
            using (InlandMarinaContext dB = new InlandMarinaContext())
            {
                docks = dB.Docks.ToList();
            }
            return docks;
        }
    }
}
