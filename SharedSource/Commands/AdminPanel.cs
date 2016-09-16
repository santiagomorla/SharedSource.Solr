using System;
using Sitecore.Configuration;
using Sitecore.Globalization;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;

namespace SharedSource.Solr.Commands
{
    [Serializable]
    public class AdminPanel : Command
    {
        public AdminPanel()
        {
        }

        /// <summary>
        /// Executes the command in the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            //Check if Solr is enabled
            string contentSearch = Settings.GetSetting("ContentSearch.Provider");
            if (string.IsNullOrWhiteSpace(contentSearch) || !contentSearch.Equals("solr", StringComparison.InvariantCultureIgnoreCase))
            {
                string message = Translate.Text("Solr is not enabled!");
                SheerResponse.Alert(message);
                return;
            }
            
            //Get Solr 
            string serviceBaseAddress = Settings.GetSetting("ContentSearch.Solr.ServiceBaseAddress");
            if (string.IsNullOrWhiteSpace(serviceBaseAddress))
            {
                string message = Translate.Text("Service Base Address is not defined!");
                SheerResponse.Alert(message);
                return;
            }

            //Open Solr Administration UI 
            UrlString webSiteUrl = new UrlString(serviceBaseAddress);
            SheerResponse.Eval("window.open('" + webSiteUrl + "', '_blank')");
        }
    }
}