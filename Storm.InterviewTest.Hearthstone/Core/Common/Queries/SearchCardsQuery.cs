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

		public SearchCardsQuery(string q)
		{
			_q = q ?? string.Empty;
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
            return queryOver.Where(x => x.Name.Contains(_q) || x.Type.ToString() == _q || x.PlayerClass == Capitalize(_q));

        }
    }
}