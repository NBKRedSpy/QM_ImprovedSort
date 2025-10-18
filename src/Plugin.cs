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

            State = modContext.State;

            Config = ModConfig.LoadConfig(ConfigDirectories.ConfigPath, Logger);

            McmConfiguration = new McmConfiguration(Config, Logger);
            McmConfiguration.TryConfigure();

            new Harmony("nbk_redspy.ImprovedSort").PatchAll();
        }
    }
}
