using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace RefIdSP
{
    public class RefIdHelper
    {
        private const string ParatureSsoBaseUrl = "https://sso.parature.com";
        private const string ParaturePickupUrl = ParatureSsoBaseUrl + "/ext/ref/pickup?REF={0}";
        private const string InstanceIdParamName = "ping.instanceId";

        public static JObject PickUp(string refId, string username, string password, string instanceId)
        {
            //Request our CSR identity
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
                //store CSR in JSON
                json = reader.ReadToEnd();
                reader.Close();
            }

            //Parse our CSR
            var jsonObj = JObject.Parse(json);
            return jsonObj;
        }
    }
}