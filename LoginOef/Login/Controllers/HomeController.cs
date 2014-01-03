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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bands()
        {
            BandSQLRepository myDbHandler = new BandSQLRepository(); 
            return View(myDbHandler.GetAllBands());
        }

        public ActionResult Details(int id)
        {
            BandSQLRepository myDbHandler = new BandSQLRepository();

            var myModel = myDbHandler.getBandSpecific(id);

            ViewBag.name = myModel.naam;

            return View(myModel);
        }

        public ActionResult Tickets()
        {
            TicketSQLRepository myDbHandler = new TicketSQLRepository();
            ViewBag.Tickets = myDbHandler.getAllTickets();
            var BestellingList = new List<DagBestelling>();
            return View(BestellingList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Tickets(List<DagBestelling> model)
        {
            //Geprobeert extra variable soort in te stellen in view, maar niet in geslaagd,
            //het probleem zijnde dat bij Model[i] de i out of bound is, terwijl in eenzelfde
            //Codeblock m => m[i] wel werkt??
             
            //Fix because it didn't work :( #saddayQQ
            model[0].soort = "vrijdag";
            model[1].soort = "zaterdag";
            model[2].soort = "zondag";
            model[3].soort = "Comboticket (3 dagen)";
            model[4].soort = "Parking";

            TicketSQLRepository myDbHandler = new TicketSQLRepository();
            myDbHandler.insertOrder(model, User.Identity.Name);
            
            return View("TicketBesteld", model);
        }

        public ActionResult BesteldeTickets()
        {
            TicketSQLRepository myDbHandler = new TicketSQLRepository();
            return View(myDbHandler.getAllTicketsByUser(User.Identity.Name));
        }

        public ActionResult Ticketstatus() 
        {
            TicketSQLRepository myDbHandler = new TicketSQLRepository();

            return View(myDbHandler.getAllTickets()); 
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
