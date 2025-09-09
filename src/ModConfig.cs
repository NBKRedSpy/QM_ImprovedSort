using QM_ImprovedSort.Utility.Mcm;
using QM_ImprovedSort.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using UnityEngine;


namespace QM_ImprovedSort;

public class ModConfig : PersistentConfig<ModConfig>, IMcmConfigTarget
{


    /// <summary>
    /// If true, the sorting will group items by their type.  Ex: "army_", "pmc_", etc.  This is similar
    /// to the game's default sort.
    /// </summary>
    public bool GroupByManufacture { get; set; } = true;

    public ModConfig() 
    {
    }


    public ModConfig(string configPath, Utility.Logger logger) : base(configPath, logger)
    {
    }

}
