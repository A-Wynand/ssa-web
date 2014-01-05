using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Festival.Models;
using Festival.Models.DAL;

namespace Festival.Controllers
{
    public class HomeController : Controller
    {

        // GET: /Home/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Home/Bands
        public ActionResult Bands()
        {
            BandSQLRepository myDbHandler = new BandSQLRepository(); 
            return View(myDbHandler.GetAllBands());
        }

        // GET: /Home/Details/ID (bands)
        public ActionResult Details(int id)
        {
            BandSQLRepository myDbHandler = new BandSQLRepository();

            var myModel = myDbHandler.getBandSpecific(id);

            ViewBag.name = myModel.naam;

            return View(myModel);
        }

        // GET: /Home/Tickets
        public ActionResult Tickets()
        {
            TicketSQLRepository myDbHandler = new TicketSQLRepository();
            ViewBag.Tickets = myDbHandler.getAllTickets();

            var BestellingList = new List<DagBestelling>();
            return View(BestellingList);
        }

        // POST: /Home/Tickets
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Tickets(List<DagBestelling> model)
        {
            // Geprobeerd extra variable soort in te stellen in view, maar niet in geslaagd,
            // het probleem zijnde dat bij Model[i] de i out of bound is, terwijl in eenzelfde
            // Codeblock m => m[i] wel werkt??
             
            // Alt fix, statisch oplossen
            model[0].soort = "vrijdag";
            model[1].soort = "zaterdag";
            model[2].soort = "zondag";
            model[3].soort = "Comboticket (3 dagen)";
            model[4].soort = "Parking";

            // Kijken of aantal binnengekomen tickets niet groter is dan gekochte tickets
            TicketSQLRepository myDbHandler = new TicketSQLRepository();
            
            var allTickets = myDbHandler.getAllTickets();
            foreach (var item in allTickets)
            {
                foreach (var modelitem in model)
                {
                    if (modelitem.soort == item.soort)
                    {
                        if (modelitem.aantal > item.aantal)
                        {
                            return View("errorTicketOrder");
                        }
                    }
                }
            }
            //Als we hier doorraken dan is alles safe & local & serverside zijn protected
            

            myDbHandler.insertOrder(model, User.Identity.Name);
            
            return View("TicketBesteld", model);
        }

        // GET: /Home/BesteldeTickets
        public ActionResult BesteldeTickets()
        {
            TicketSQLRepository myDbHandler = new TicketSQLRepository();
            return View(myDbHandler.getAllTicketsByUser(User.Identity.Name));
        }

        // GET: /Home/Ticketstatus
        public ActionResult Ticketstatus() 
        {
            TicketSQLRepository myDbHandler = new TicketSQLRepository();

            return View(myDbHandler.getAllTickets()); 
        }

        // GET: /Home/Contact
        public ActionResult Contact()
        {
            return View();
        }
    }
}
