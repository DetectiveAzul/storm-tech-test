using System.Collections.Generic;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards.Domain;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards.Models;

namespace Storm.InterviewTest.Hearthstone.Core.Features.Cards
{
	public interface ICardSearchService
	{
		CardModel FindById(string id);
        //Interface need to be modified to accept the new filter
		IEnumerable<CardModel> Search(string searchTerm, string hero);
		IEnumerable<CardModel> GetHeroes();
	}
}