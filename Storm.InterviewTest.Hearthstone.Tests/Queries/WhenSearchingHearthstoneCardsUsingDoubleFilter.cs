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
    public class WhenSearchingHearthstoneCardsUsingDoubleFilter : HearthstoneCardCacheContext
    {
        protected IEnumerable<ICard> _result;
        protected IEnumerable<ICard> _result2;
        protected string query;
        protected string query2;
        protected string hero;

        protected override IEnumerable<ICard> Cards()
        {
            return new List<ICard>(base.Cards())
            {
                CreateRandomMinionCardWithId("99", minion =>
                {
                    minion.Name = "my random card";
                    minion.Faction = FactionTypeOptions.Alliance;
                    minion.Rarity = RarityTypeOptions.Legendary;
                    minion.PlayerClass = "Warrior";
                })
            };
        }

        protected override void Context()
        {
            query = "leroy";
            query2 = "";
            hero = "Warrior";
        }

        protected override void Because()
        {
            _result = _hearthstoneCardCache.Query(new SearchCardsQuery(query, hero));
            _result2 = _hearthstoneCardCache.Query(new SearchCardsQuery(query2, hero));
        }

        //This test should not return results because from the Warrior cards we got from the query (4), none of
        //them are called 'Leroy'. 
        [Test]
        public void ShouldReturnNoResults()
        {
            _result.Count().ShouldEqual(0);
        }

        //This should return one card at least because we did not set a query term to look within the Warrior cards
        [Test]
        public void ShouldReturnOneResult()
        {
            _result2.Count().ShouldEqual(1);
        }
    }
}