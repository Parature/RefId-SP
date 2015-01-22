using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace RefIdSP
{
    /// <summary>
    /// Helper class for picking up the JSON claims information from the Parature server
    /// </summary>
    public class RefIdHelper
    {
        /*
         * These are currently hardcoded as they have not changed in several years.
         * New servers may be added at some point in the future, but this code will be updated when this happens
         */
        private const string ParatureSsoBaseUrl = "https://sso.parature.com";
        private const string ParaturePickupUrl = ParatureSsoBaseUrl + "/ext/ref/pickup?REF={0}";
        private const string InstanceIdParamName = "ping.instanceId";

        /// <summary>
        /// Pick up the JSON claims from the Parature authentication server
        /// </summary>
        /// <param name="refId">
        /// The reference Id token passed in the query string from the Parature Authentication server.
        /// </param>
        /// <param name="username">
        /// Username used to authenticate to the Parature Authentication server when retrieving the actual claims.
        /// </param>
        /// <param name="password">
        /// Username used to authenticate to the Parature Authentication server when retrieving the actual claims.
        /// </param>
        /// <param name="instanceId">
        /// The reference Id adapter created by Parature for this SSO configuration. 
        /// </param>
        /// <returns></returns>
        public static JObject PickUp(string refId, string username, string password, string instanceId)
        {
            //Request from the Parature Server the claims information specified by the unique refId
            var req = (HttpWebRequest)WebRequest.Create(string.Format(ParaturePickupUrl, refId));
            req.Credentials = new NetworkCredential(username, password);
            req.PreAuthenticate = true;
            req.Headers.Add(InstanceIdParamName, instanceId);

            var responseFromPing = req.GetResponse().GetResponseStream();
            if (responseFromPing == null)
            {
                return null;
            }

            string json;
            using (var reader = new StreamReader(responseFromPing))
            {
                //read the response
                json = reader.ReadToEnd();
                reader.Close();
            }

            //Parse to a json object
            var jsonObj = JObject.Parse(json);
            return jsonObj;
        }
    }
}