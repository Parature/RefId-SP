﻿using System;
using System.Configuration;
using System.Web;
using System.Web.SessionState;

/*
 * How To:
 * 1. Set up Portal as an IdP using the Intranet. (Must be performed by a Parature Technician)
 * 2. (This is performed by a Parature Technician) Set the :
 *  2a. ACS Url to the url of this website. This can be changed later. During testing it will likely be a localhost address
 *  2b. SP Id - Unique ID for this sso instance (department specific). 
 *      Likely never changes. Needs to be formatted as a URI, but is not used at all except in the below link
 * 3. Trigger an IdP initiated SSO:
 *  https://sso.parature.com/idp/startSSO.ping?PartnerSpId={YOUR-SP-NAME-IN-PING}
 *      Where {YOUR-SP-NAME-IN-PING} is the "EntityId" in the webconfig, must be globally unique in our system, and is purely an id (not actually used)
 *      
 * This will kick off IdP initiated SSO and end at the SPSiteUrl.
 */
namespace RefIdSP
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        { }

        void Application_End(object sender, EventArgs e)
        { }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            /*
             * If the query string has a "REF", it is a token to go pick up the Claims from the parature SSO server
             */
            var refId = Request.QueryString["REF"];
            if (string.IsNullOrEmpty(refId) == false)
            {
                //SSO settings to talk to the parature SSO server
                var username = ConfigurationManager.AppSettings["ssoUsername"];
                var password = ConfigurationManager.AppSettings["ssoPassword"];
                var instanceId = ConfigurationManager.AppSettings["ssoInstanceId"];

                //get the claims information. 
                //This requires a round-trip request from the parature SSO server
                var json = RefIdHelper.PickUp(refId, username, password, instanceId);
                var test = "";
            }
        }

        void Application_Error(object sender, EventArgs e)
        { }
    }
}
