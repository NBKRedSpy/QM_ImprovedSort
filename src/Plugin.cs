using HarmonyLib;
using MGSC;
using ImprovedSort.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImprovedSort_Bootstrap;

namespace ImprovedSort
{
    public class Plugin : BootstrapMod
    {

        public static ConfigDirectories ConfigDirectories = new ConfigDirectories();

        public static State State { get; private set; }
        public static ModConfig Config { get; private set; }
        public static Utility.Logger Logger = new();
        private static McmConfiguration McmConfiguration;

        public Plugin(HookEvents hookEvents, bool isBeta) : base(hookEvents, isBeta)
        {

            this.HookEvents.AfterConfigsLoaded += OnAfterConfigsLoaded;
        }

        private void OnAfterConfigsLoaded(IModContext context)
        {
            Config = ModConfig.LoadConfig(ConfigDirectories.ConfigPath, Logger);

            State = context.State;

            McmConfiguration = new McmConfiguration(Config, Logger);
            McmConfiguration.TryConfigure();

            new Harmony("nbk_redspy.ImprovedSort").PatchAll();
        }
    }
}
