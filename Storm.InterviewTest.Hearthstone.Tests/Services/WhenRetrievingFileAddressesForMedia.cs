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
        protected string localBaseDirectory;
        protected string id;
        protected MediaRetrievalService mediaService;
       


        public WhenRetrievingFileAddressesForMedia()
        {
            mediaSourceUrl = "http://wow.zamimg.com/images/hearthstone/cards/enus/medium/{0}.png";
            mediaPath = "~/App_Data/media/";
            namingFormat = "{0}.png";
            //This is a fake localBaseDirectory, on the real call we pass this parameter by calling
            //CreateDocument() (a method on the service) from the Controller that uses the service. 

            localBaseDirectory = "C:/ProgramFiles/App_Data/media/";
            id = "1";

            mediaService = new MediaRetrievalService(mediaSourceUrl, mediaPath);
            
           
            

        }

        [Test]
        public void ShouldCreateACorrectUrlWithNumericId()
        {
            var result = mediaService.getFile(namingFormat, id, localBaseDirectory);
            result.ShouldEqual("C:/ProgramFiles/App_Data/media/1.png");
        }

    }
}