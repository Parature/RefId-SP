using System;
using System.Configuration;
using System.Web.UI;

namespace RefIdSP
{
    public partial class Status : Page
    {
        private static readonly Uri SsoStartLink = new Uri("https://sso.parature.com/sp/startSSO.ping");

        protected void Page_Load(object sender, EventArgs e)
        {
            ManageViewState();
        }

        public void RunSso(Object sender, EventArgs e)
        {
            if (SessionManager.Name != null)
            {
                SessionManager.Name = null;
            }
            else
            {
                //This link depends heavily on your configuration
                var url = new Uri(SsoStartLink, 
                    string.Format("?PartnerIdpId={0}&TARGET={1}", ConfigurationManager.AppSettings["ssoIdPUrl"], Request.Url.OriginalString));
                Response.Redirect(url.AbsoluteUri);
            }

            ManageViewState();
        }

        private void ManageViewState()
        {
            var name = SessionManager.Name;
            if (name == null)
            {
                AuthStatus.Text = "Unauthenticated";
                Name.Text = string.Empty;
                startSSO.Text = "Start SSO";
            }
            else
            {
                AuthStatus.Text = "Authenticated";
                Name.Text = name;
                startSSO.Text = "Logout";
            }       
        }
    }
}