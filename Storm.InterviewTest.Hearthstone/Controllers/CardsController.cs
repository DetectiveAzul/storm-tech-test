using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards.Services;

namespace Storm.InterviewTest.Hearthstone.Controllers
{
    public class CardsController : Controller
    {
		public ActionResult Index(string q = null, string hero = null)
		{
			var searchService = new CardSearchService(MvcApplication.CardCache);

            var cards = searchService.Search(q, hero);

			return View(cards);
		}
	}
}