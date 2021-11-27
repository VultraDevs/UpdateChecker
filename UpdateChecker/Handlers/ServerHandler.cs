using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;

namespace UpdateChecker.Handlers
{
    public class ServerHandler
    {

        public void OnWaitingForPlayers()
        {
            if (Plugin.NeedARestart)
            {
                Log.Info("Restarting server for an plugin auto-update.");
                Server.Restart();
                
            }
        }

        public void OnPlayerJoined()
        {
            if (Plugin.NeedARestart)
            {
                Log.Info("Restarting server for an plugin auto-update.");
                Server.Restart();
            }
        }
    }
}
