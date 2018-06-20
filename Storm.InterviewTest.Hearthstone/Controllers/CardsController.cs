using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards.Services;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards.Domain;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards.Models;

namespace Storm.InterviewTest.Hearthstone.Controllers
{
    public class CardsController : Controller
    {
		public ActionResult Index(string q = null, string hero = null)
		{
			var searchService = new CardSearchService(MvcApplication.CardCache);

            var cards = searchService.Search(q, hero);

            //Getting all the hero cards to create the dropdown menu by code
            //I wanted to avoid hard coding all the hero selections

            var heroes = searchService.GetHeroes();

            // Creating an object with the whole set of cards and heroes to pass to the view
            var viewData = new ViewDataModel();
            viewData.CardsData = cards;
            viewData.HeroesData = heroes;

            //Return the new object to the view
            return View(viewData);
		}
	}
}