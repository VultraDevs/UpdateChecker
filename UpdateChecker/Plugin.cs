using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Newtonsoft.Json;

namespace UpdateChecker
{
    public class Plugin : Plugin<Config>
    {

        public string TagVersion = "v1.0.1";
        
        private string GetReleases()
        {
            const string GITHUB_API = "https://api.github.com/repos/VultraDevs/UpdateChecker/releases";
            WebClient webClient = new WebClient();
            webClient.Headers.Add("User-Agent", "Unity web player");
            Uri uri = new Uri(string.Format(GITHUB_API, "VultraDevs", "UpdateChecker"));
            string releases = webClient.DownloadString(uri);
            return releases;
        }


        public override void OnEnabled()
        {
            Log.Info("Checking for updates.");
            if (GetReleases().Contains(TagVersion)) 
            {
                Log.Info("You're on the latest version.");

            } else
            {
                string DownloadURL = "https://github.com/VultraDevs/UpdateChecker/releases/download/+ " + $"{TagVersion}" + "/UpdateChecker.dll";
                //string DownloadURL = $"https://github.com/VultraDevs/UpdateChecker/releases/download/" + $"{TagVersion}" + "/UpdateChecker.dll";
                Log.Info("You're not on the latest version. Going to auto update for you if you have the config set.");
                File.Delete(Paths.Plugins + "/UpdateChecker.dll");
                var fileName = Path.Combine(Paths.Plugins, "UpdateChecker.dll");
                Log.Debug("Going to pass through a webclient to download the latest version.", Config.IsDebugEnabled);
                using (var client = new WebClient())
                {

                    client.DownloadFile(DownloadURL, fileName);
                }
                Log.Debug("Server restart needed to take effect", Config.IsDebugEnabled);
                
            }
            Log.Debug("Done checking for updates.", Config.IsDebugEnabled);
        }

        public override void OnDisabled()
        {
            Log.Info("Plugin turning off");

        }
    }
}
