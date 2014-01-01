using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login.Models;
using Login.Models.DAL;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Bands()
        {
            Console.WriteLine(" HERE WE GO LOOK FOR ME : ");
            BandSQLRepository myDbHandler = new BandSQLRepository(); 
            return View(myDbHandler.GetAllBands());
        }

        public ActionResult Details(int id)
        {
            BandSQLRepository myDbHandler = new BandSQLRepository();
            Console.WriteLine(" HERE WE GO LOOK FOR ME : " + id);

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

        public ActionResult Ticketstatus() 
        {
            TicketSQLRepository myDbHandler = new TicketSQLRepository();

            return View(myDbHandler.getAllTickets()); 
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
