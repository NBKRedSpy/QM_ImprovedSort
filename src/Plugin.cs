using HarmonyLib;
using MGSC;
using QM_ImprovedSort.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_ImprovedSort
{
    public static class Plugin
    {

        public static ConfigDirectories ConfigDirectories = new ConfigDirectories();
        public static ModConfig Config { get; private set; }
        public static Utility.Logger Logger = new();
        private static McmConfiguration McmConfiguration;


        [Hook(ModHookType.AfterBootstrap)]
        public static void Init(IModContext modContext)
        {
            Config = ModConfig.LoadConfig(ConfigDirectories.ConfigPath, Logger);
            
            McmConfiguration = new McmConfiguration(Config, Logger);
            McmConfiguration.TryConfigure();


            new Harmony("nbk_redspy.QM_ImprovedSort").PatchAll();
        }
    }
}
