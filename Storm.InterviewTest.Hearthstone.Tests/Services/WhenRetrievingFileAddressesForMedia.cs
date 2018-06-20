using NUnit.Framework;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards.Services;
using Storm.InterviewTest.Hearthstone.Tests.Specification;

namespace Storm.InterviewTest.Hearthstone.Tests.Services
{
    [TestFixture]
    public class WhenRetrievingFileAddressesForMedia
    {
        protected string mediaSourceUrl;
        protected string mediaPath;
        protected string namingFormat;
        protected string id;
        protected MediaRetrievalService mediaService;
       


        public WhenRetrievingFileAddressesForMedia()
        {
            mediaSourceUrl = "http://wow.zamimg.com/images/hearthstone/cards/enus/medium/{0}.png";
            mediaPath = "~/App_Data/media/";
            mediaService = new MediaRetrievalService(mediaSourceUrl, mediaPath);
            namingFormat = "{0}.png";
            id = "1";

        }

        [Test]
        public void ShouldCreateACorrectUrlWithNumericId()
        {
            var result = mediaService.getFile(namingFormat, id);
            result.ShouldEqual("{0}.png");
        }

    }
}