using System;
using System.Collections.Generic;
using System.Linq;
using Storm.InterviewTest.Hearthstone.Core.Common.Queries.Base;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards.Domain;

namespace Storm.InterviewTest.Hearthstone.Core.Common.Queries
{
	public class SearchCardsQuery : CardListLinqQueryObject<ICard>
	{
		private readonly string _q;
        //Added a new variable to handle the hero filter from Cards browser view
        private readonly string _hero;

		public SearchCardsQuery(string q, string hero)
		{
			_q = q ?? string.Empty;
            //It will add the hero filter to our query
            _hero = hero ?? string.Empty;
		}

        //This method would refactor a string Q to a version which initial is upperCased. This is used to compare a input
        //like 'mage' with the playerClass (that are written on uppercase, example: 'Mage'). In this way, if you use 'mage'
        //all cards containing the mage word and all the Mage cards should appear, while using 'Mage' would only select
        //cards from that class.

        private string Capitalize(string q)
        {
            return char.ToUpper(q[0]) + q.Substring(1);
        }


        protected override IEnumerable<ICard> ExecuteLinq(IQueryable<ICard> queryOver)
        {
            //Adding an extra check to not return cards which type is 'Hero' would block hero cards of being displayed on the card browser
            //Since these heroes can be search through another method on CardSearchService.getHeroes() we don't lose the ability to get them

            //We query all the cards
            //Then, if the hero filter is not null or empty, first action would select all the cards
            //from the class selected on the dropdown menu (or all of them)
            //Then it will filter them using the searchbox next to the dropdown menu

            IEnumerable<ICard> allCards = queryOver;
            if (_hero != string.Empty)
            {
                allCards = queryOver.Where(x => x.PlayerClass == _hero );
            }
            return allCards.Where(x => ( x.Name.Contains(_q) || x.Type.ToString() == _q || x.PlayerClass == Capitalize(_q)) && x.Type.ToString() != "Hero");
        }
    }
}