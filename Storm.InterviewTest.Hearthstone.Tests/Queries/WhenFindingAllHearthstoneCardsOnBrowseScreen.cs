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
                    hero.PlayerClass = "Paladin";

                })
            };
        }

        protected override void Context()
        {
            query = "Paladin";
        }

        protected override void Because()
        {
            _result = _hearthstoneCardCache.Query(new SearchCardsQuery(query));
        }

        //Explanation of the test: The cards already on the CardCache are not shaman card neither are
        //heroes cards So, on a normal situation, our Query asking for a Paladin card should at least
        //return the card created on this test. But, since the method for getting the cards using the 
        //searchbar (called through SearchCardsQuery) has been refactored to block returning heroes on this
        //screen, and our card is a hero, the result we expect is 0. 
        [Test]
        public void ShouldReturnExpectedSearchResults()
        {
            _result.Count().ShouldEqual(0);
        }
    }
}