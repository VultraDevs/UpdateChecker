using Exiled.API.Interfaces;
using System.ComponentModel;

namespace UpdateChecker
{
    public class Config : IConfig
    {
        [Description("Is this plugin enabled")]
        public bool IsEnabled { get; set; } = true;
        [Description("If plugin enable it will update the plugin automatically. If not enabled it will just tell you it needs a update.")]
        public bool AutoUpdate { get; set; } = true;
        [Description("Is debug mode enabled? If so, this will spam a lot into your console.")]
        public bool IsDebugEnabled { get; set; } = true;
    }
}
