using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards.Domain;

//This class was created to group all the information I want to pass to the Card Browser view to simplify
//how we send information to the view (that only accepts one parameter now)
namespace Storm.InterviewTest.Hearthstone.Core.Features.Cards.Models
{
    public class ViewDataModel
    {
        public IEnumerable<CardModel> CardsData { get; set; }
        public IEnumerable<CardModel> HeroesData { get; set;  }

        public ViewDataModel()
        {

        }

    }
}