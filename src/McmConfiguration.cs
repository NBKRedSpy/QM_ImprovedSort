using ImprovedSort.Utility;
using ImprovedSort.Utility.Mcm;
using ImprovedSort;
using ModConfigMenu;
using ModConfigMenu.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace ImprovedSort
{
    internal class McmConfiguration : McmConfigurationBase
    {

        public McmConfiguration(ModConfig config, Utility.Logger logger) : base (config, logger) { }

        public override void Configure()
        {
            ModConfigMenuAPI.RegisterModConfig("Improved Sort", new List<ConfigValue>()
            {
                CreateConfigProperty(nameof(ModConfig.GroupByManufacture),
                    "Disable this option to sort items by price, placing the most expensive items first. " +
                    "This is often useful because the most expensive item is usually the best. " +
                    "If enabled, items will be grouped by type, similar to the game's default sorting (e.g., energy weapons grouped together)."
                )
            }, OnSave);
        }
    }
}
