using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    /// <summary>
    /// repository of all slip information and access to database
    /// </summary>
    public class SlipsManager
    {
        /// <summary>
        /// gets a list of all unleased slips
        /// </summary>
        /// <returns>returns a list of all unleased slips by checking first which slips are present in the lease table or none if null</returns>
        public static List<Slip> GetSlips() 
        {
            List<Slip> slips = null;
            using (InlandMarinaContext dB = new InlandMarinaContext())
            {
                // Retrieve the list of SlipIDs from the Lease table
                List<int> leasedSlipIDs = dB.Leases.Select(l => l.SlipID).ToList();

                // Use the Where() method to filter the Slip records by SlipID
                slips = dB.Slips.Include(d => d.Dock).Where(s => !leasedSlipIDs.Contains(s.ID)).ToList();
            }
            return slips;
        }
        
        /// <summary>
        /// gets a list of slips by a given dock
        /// </summary>
        /// <param name="id">the id of the given dock</param>
        /// <returns>return a list of slips by a given dock or null if none</returns>
        public static List<Slip> GetSlipsByDock(int id)
        {
            List<Slip> slips = null;
            using (InlandMarinaContext dB=new InlandMarinaContext())
            {
                slips = dB.Slips.Where(s => s.DockID == id).
                    Include(d => d.Dock).OrderBy(d => d.ID).ToList();
            }
            return slips;
        }

        
    }
}
