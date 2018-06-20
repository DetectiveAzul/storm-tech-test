using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Storm.InterviewTest.Hearthstone.Core.Features.Cards.Services;

namespace Storm.InterviewTest.Hearthstone.Controllers
{
    public class MediaController : Controller
    {
	    private const string mediaSourceUrl = "http://wow.zamimg.com/images/hearthstone/cards/enus/medium/{0}.png";
        private const string mediaPath = "~/App_Data/media/";
        private const string namingFormat = "{0}.png";

        // GET: Media
        public ActionResult Card(string id)
        {
            //Create the service
            var mediaService = new MediaRetrievalService(mediaSourceUrl, mediaPath);
            //Get the localbaseDirectory (using the current HTTP Context)
            var localBaseDirectory = mediaService.createDirectory();
            //Create the path to the file
            var localFile = mediaService.getFile(namingFormat, id, localBaseDirectory);
            //Download the file if not exists on the local directory
            mediaService.DownloadFromSource(id, localFile);
            //Return the file to the view
            return File(localFile, "image/png");
            
		}

    } 
}