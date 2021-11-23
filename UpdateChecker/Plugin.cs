using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;

namespace UpdateChecker
{
    public class Plugin : Plugin<Config>
    {
        
        public override void OnEnabled()
        {
            Log.Info("Checking for updates.");

            Log.Debug("Done checking for updates.", Config.IsDebugEnabled);
        }

        public override void OnDisabled()
        {
            Log.Info("Plugin turning off");

        }
    }
}
