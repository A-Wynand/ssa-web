using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FestivalWeb.Models;


namespace FestivalWeb.Controllers
{
    public class GebruikersController : Controller
    {
        //
        // GET: /Gebruikers/

        public ActionResult Index()
        {
            GebruikersDBHandler myDbHandler = new GebruikersDBHandler();

            ViewBag.message = myDbHandler.getAllGebruikers();
            return View();
        }

    }
}
