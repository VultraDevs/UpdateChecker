using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Newtonsoft.Json;
using UpdateChecker.Handlers;
using MEC;
using ServerEvent = Exiled.Events.Handlers.Server;

namespace UpdateChecker
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "UpdateChecker";
        public override string Author { get; } = "Loopy";
        public override string Prefix { get; } = "UpdateChecker";
        public override Version RequiredExiledVersion { get; } = new Version(3,7,2);
        public override Version Version { get; } = new Version(1,0,2);
        public override PluginPriority Priority { get; } = PluginPriority.First;

        public string TagVersion = "v1.0.2";
        public static bool NeedARestart = false;

        private ServerHandler ServerHandler;
        private string GetReleases()
        {
            const string GITHUB_API = "https://api.github.com/repos/VultraDevs/UpdateChecker/releases";
            WebClient webClient = new WebClient();
            webClient.Headers.Add("User-Agent", "Unity web player");
            Uri uri = new Uri(string.Format(GITHUB_API, "VultraDevs", "UpdateChecker"));
            string releases = webClient.DownloadString(uri);
            return releases;
        }
        public IEnumerator<float> UpdatePlugin()
        {
            for (; ; )
            {
                if (GetReleases().Contains(TagVersion))
                {
                    

                }
                else
                {
                    string DownloadURL = "https://github.com/VultraDevs/UpdateChecker/releases/download/" + $"{TagVersion}" + "/UpdateChecker.dll";
                    Log.Info("You're not on the latest version. Going to auto update for you if you have the config set.");
                    File.Delete(Paths.Plugins + "/UpdateChecker.dll");
                    var fileName = Path.Combine(Paths.Plugins, "UpdateChecker.dll");

                    if (Config.AutoUpdate)
                    {
                        Log.Debug("Going to pass through a webclient to download the latest version.", Config.IsDebugEnabled);
                        using (var client = new WebClient())
                        {

                            client.DownloadFile(DownloadURL, fileName);
                        }
                        Log.Debug("Server restart needed to take effect.", Config.IsDebugEnabled);
                        foreach (Player Ply in Player.List)
                        {
                            Ply.ShowHint("<color=green>Server restart taking effect for a plugin update.</color>", 15);
                        }
                        NeedARestart = true;
                    }
                    else
                    {
                        Log.Debug("Not going to auto-update due to the config being off.", Config.IsDebugEnabled);
                    }
                }
                Log.Debug("Done checking for updates you should be on the latest version now!", Config.IsDebugEnabled);
                yield return Timing.WaitForSeconds(1f);
            }
        }

        public override void OnEnabled()
        {
            Timing.RunCoroutine(UpdatePlugin());
            ServerHandler = new ServerHandler();
            ServerEvent.WaitingForPlayers += ServerHandler.OnWaitingForPlayers;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Log.Info("Plugin turning off");
            ServerEvent.WaitingForPlayers -= ServerHandler.OnWaitingForPlayers;
            ServerHandler = null;
            base.OnDisabled();

        }
    }
}
