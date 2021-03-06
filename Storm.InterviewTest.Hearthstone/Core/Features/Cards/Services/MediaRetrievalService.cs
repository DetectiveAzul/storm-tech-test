﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace Storm.InterviewTest.Hearthstone.Core.Features.Cards.Services
{
    public class MediaRetrievalService
    {
        private string mediaSourceUrl;
        private string mediaPath;

        public MediaRetrievalService(string mediaSourceUrl, string mediaPath)
        {
            this.mediaSourceUrl = mediaSourceUrl;
            this.mediaPath = mediaPath;
        }

        //This is the main method, it takes the naming format: {0}.png and the id for the media.
        //Then it call helper method createDirectory. This create the localDirectory url string.
        //With this string, we combine it with the created Filename and, if it not exists, we will
        //downloaded it from the source. Then return the localFile full url to the Controller. 
        public string getFile(string namingFormat, string id, string localBaseDirectory)
        {
            var cardFileName = string.Format(namingFormat, id);
            //Originally I had the call to createDirectory() here, but it is using a HttpContext, that
            //only works when logged in. Unit tests were not available to test the feature like this, so
            //I pass the responsability of getting this to the controller, that works using HTTP contexts
            var localFile = Path.Combine(localBaseDirectory, cardFileName);


            return localFile;
        }

        //Since we add the path as a class variable (to increase reusability), we don't need to pass
        //any parameter. 
        public string createDirectory()
        {
            //HTTPContext.Current.Server.MapPath substitutes Server.MapPath because it cannot be used
            //on a class. 
            var localBaseDirectory = HttpContext.Current.Server.MapPath(this.mediaPath);
            Directory.CreateDirectory(localBaseDirectory);
            return localBaseDirectory;
        }

        //Extracted directly from the MediaController
        public void DownloadFromSource(string cardId, string localFile)
        {
            if (!System.IO.File.Exists(localFile))
            {
            var client = new WebClient();
            client.DownloadFile(string.Format(this.mediaSourceUrl, cardId), localFile);
            }
        }


    }
}
