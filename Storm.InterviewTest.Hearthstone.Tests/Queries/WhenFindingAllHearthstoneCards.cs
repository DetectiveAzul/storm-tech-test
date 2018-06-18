using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Storm.InterviewTest.Hearthstone.Core.Common.Queries;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards.Domain;
using Storm.InterviewTest.Hearthstone.Tests.Base;
using Storm.InterviewTest.Hearthstone.Tests.Specification;

namespace Storm.InterviewTest.Hearthstone.Tests.Queries
{
	[Category("Cache")]
	public class WhenFindingAllHearthstoneCards : HearthstoneCardCacheContext
	{
		protected IEnumerable<ICard> _result;

		protected override void Because()
		{
			_result = _hearthstoneCardCache.Query(new FindAllCardsQuery());

		}

		[Test]
		public void ShouldReturnExpectedCountOfCards()
		{
			_result.Count().ShouldEqual(_cards.Count());
		}
	}
}