
using System.Collections.Generic;
using System;
using System.IO;
using System.Web;
using System.Linq;
using Newtonsoft.Json.Linq;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards.Domain;

namespace Storm.InterviewTest.Hearthstone.Core.Features.Cards.Services
{
	public class LocalJsonFeedHearthstoneCardCacheFactory : HearthstoneCardCacheFactory
	{
        private IHearthstoneCardParser parser;
        private JToken cards;

		public LocalJsonFeedHearthstoneCardCacheFactory(IHearthstoneCardParser parser) : base(parser)
		{
            this.parser = parser; 
		}

        //The refactoring has been made within two principles in mind: 
        // 1. Single responsability: Leaving methods to do the minor number of task possibles
        // 2. Readibility: Good naming and fewer braces
        // 3. Maintainability: Making methods that can be used and easy to modify
        // 4. Avoid repetition: Self explanatory

            //If we have more than one posible path, this method can be re-used to open json files
            //just changing the path string

        private StreamReader OpenTextReader(string path)
        {
            return File.OpenText(HttpContext.Current.Server.MapPath(path));
        }

        //Making the parsing more readable. It could return a null object, but main method check that
        //before using it
        private ICard ParseCard(JToken card)
        {
            var parsedCard = this.parser.Parse(card.ToString());
            return parsedCard;
        }

        //This is repeated several times over the main method, so I extracted it on a single method
        //That accepts the collection you want to look for as a string (ex: Basic, Classic, Naxxramas...)
        private IEnumerable<ICard> ParseCardsFromCollection(JObject cardSets, string collection)
        {
            if (cardSets.TryGetValue(collection, out this.cards) && this.cards.Type == JTokenType.Array)
            {
                foreach (var card in cards)
                {
                    if (ParseCard(card) != null) yield return ParseCard(card);

                }
            }
        }

        //Pasing a string[] of collections names (if the whole list of cards on the json is expanded with newer
        //expansions you can just add these names to the array, and this method can be reused without modifying
        //it. Useful for the future.

        private IEnumerable<ICard> ParseCardsFromAllCollections(StreamReader reader, string[] collections)
        {
            var cardSets = JObject.Parse(reader.ReadToEnd());

            List<ICard> allCards = Enumerable.Empty<ICard>().ToList();
            foreach (var collection in collections)
            {
                allCards = allCards.Concat(ParseCardsFromCollection(cardSets, collection)).ToList();
            }

            return allCards;
        } 

        //Main method. It would be very useful to modify this class to accept the link and the collection
        //itself, so we can leave this method without hardcoded elements, making it re-usable. 

        //I think the best way to accomplish this is to modify this file adding a new class parameter
        //for example: string url (the url for the json) and then, on the place we call this specific parser,
        //pass the url and the collection to parse. 

        //I could extend over this possible implementation during the interview
		protected override IEnumerable<ICard> PopulateCards(IHearthstoneCardParser parser)
		{
            using (var reader = OpenTextReader("~/App_Data/cards.json"))
            {
                string[] collections = new string[] { "Basic", "Classic", "Curse of Naxxramas", "Goblins vs Gnomes" };
                return ParseCardsFromAllCollections(reader, collections);
            }
        }
	}
}