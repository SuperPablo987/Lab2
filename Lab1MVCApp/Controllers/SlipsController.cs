using InlandMarinaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


/*
 * This Controller receives requests sent to Slips View and sends them to the SlipManager class to retrieve the data from the database and send it back to the view
 * Peter Thiel
 * April 20, 2023
 */
namespace Lab1MVCApp.Controllers
{

    public class SlipsController : Controller
    {
        // GET: SlipsController displays when user goes to the slip page
        public ActionResult Index()
        {
            // for commented routing below
            //return RedirectToAction("List", "Slip");
            List<Slip> slips = null; 
            List <Dock> docks = DocksManager.GetDocks();
            var list = new SelectList(docks, "ID", "Name").ToList();
            list.Insert(0, new SelectListItem("All", "All")); // adds all as first option in the list
            ViewBag.Docks = list;

            slips = SlipsManager.GetSlips(); // all slips
            return View("FilteredList", slips);
        }
        // routing when user goes to slips page
        //[Route("{controller}s/{id?}")]
        //public IActionResult List(string id = "All")
        //{
        //    List<Slip> slips = null;
        //    using (InlandMarinaContext dB = new InlandMarinaContext())
        //    {
        //        slips = SlipsManager.GetUnleasedSlips();
        //    }

        //    return View(slips);
        //}

        // filter slips by dock
        public IActionResult FilteredList()
        {
            // prepare list of docks for the drop down list
            List<Dock> docks = DocksManager.GetDocks();
            var list = new SelectList(docks, "ID", "Name").ToList();
            list.Insert(0, new SelectListItem("All", "All")); // adds all as first option in the list
            ViewBag.Docks = list;

            List<Slip> slips = SlipsManager.GetSlips(); // all slips
            return View(slips);
        }

        [HttpPost]
        public IActionResult FilteredList(string id = "All")
        {
            // retain the docks for drop down list and selected item
            List<Dock> docks = DocksManager.GetDocks();
            var list = new SelectList(docks, "ID", "Name").ToList();
            list.Insert(0, new SelectListItem("All", "All")); // adds all as first option in the list
            foreach(var item in list) // find selected item
            {
                if(item.Value == id)
                {
                    item.Selected = true;
                    break;
                }
            }
            ViewBag.Docks = list;
            List<Slip> slips;
            if(id == "All")
            {
                slips = SlipsManager.GetSlips(); // all slips
            }
            else // a dock is selected
            {
                slips = SlipsManager.GetSlipsByDock(Convert.ToInt32(id)); // filtered docks
            }
            return View(slips);


        }

        // GET: SlipsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlipsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlipsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        // displays logged in customers slips
        public ActionResult MySlips()
        {
            int? customerID = HttpContext.Session.GetInt32("CurrentCustomer");
            List<Slip> slips = null;
            if(customerID == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else // customerID != null
            {
                slips = SlipsManager.GetMySlips((int)customerID);
            }
            
            return View(slips); 
        }


        // GET: SlipsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SlipsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize]
        // GET: SlipsController/Lease/5
        public ActionResult Lease(int id)
        {
            int? customerID = HttpContext.Session.GetInt32("CurrentCustomer");
            if (customerID == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Slip? slip = SlipsManager.SlipToLease(id);
                return View(slip);
            }
        }

        // POST: SlipsController/Lease/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Lease(int id, Lease lease)
        {
            try
            {
                lease = new Lease();
                int? customerID = HttpContext.Session.GetInt32("CurrentCustomer");
                LeaseManager.LeaseSlip(id, (int)customerID,lease);
                return RedirectToAction("MySlips", "Slips");
            }
            catch
            {
                return View();
            }
        }
    }
}
