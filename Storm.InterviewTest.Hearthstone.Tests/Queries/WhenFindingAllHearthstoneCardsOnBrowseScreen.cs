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
    public class WhenFindingAllHearthstoneCardsOnBrowseScreen : HearthstoneCardCacheContext
    {
        protected IEnumerable<ICard> _result;
        protected string query;

        protected override IEnumerable<ICard> Cards()
        {
            return new List<ICard>(base.Cards())
            {
                CreateHeroCardWithId("99", hero =>
                {
                    hero.Name = "a random hero";
                    hero.PlayerClass = "Shaman";

                })
            };
        }

        protected override void Context()
        {
            query = "Shaman";
        }

        protected override void Because()
        {
            _result = _hearthstoneCardCache.Query(new SearchCardsQuery(query));
        }

        [Test]
        public void ShouldReturnExpectedSearchResults()
        {
            _result.Count().ShouldEqual(0);
        }
    }
}