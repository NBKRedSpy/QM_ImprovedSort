using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.AccessControl;

namespace ImprovedSort.Utility;

public abstract class  PersistentConfig<T> where T : PersistentConfig<T>, new()
{
    [JsonIgnore]
    protected string ConfigPath { get; set; }

    protected Logger Logger { get; set; }

    public PersistentConfig()
    {

    }

    public virtual void Delete()
    {
        if (File.Exists(ConfigPath))
        {
            File.Delete(ConfigPath);
        }
    }

    /// <summary>
    /// The base must set the ConfigPath after construction
    /// </summary>
    /// <param name="logger"></param>
    protected PersistentConfig(Logger logger) : this("", logger) {}

    public PersistentConfig(string configPath, Logger logger)
    {
        ConfigPath = configPath;
        Logger = logger;
    }

    private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        
    };


    public static T LoadConfig(string configPath, Logger logger)
    {
        T config;
        CreateDirectory(configPath);

        if (File.Exists(configPath))
        {
            try
            {
                string sourceJson = File.ReadAllText(configPath);
                config = JsonConvert.DeserializeObject<T>(sourceJson, SerializerSettings);
                config.ConfigPath = configPath;
                config.Logger = logger;

                //Add any new elements that have been added since the last mod version the user had.
                string upgradeConfig = JsonConvert.SerializeObject(config, SerializerSettings);

                if (upgradeConfig != sourceJson)
                {
                    logger.Log("Updating config with missing elements");
                    config.Save();
                }


                return config;
            }
            catch (Exception ex)
            {
                logger.LogError("Error parsing configuration.  Ignoring config file and using defaults");
                logger.LogError(ex);

                //Not overwriting in case the user just made a typo.
                config = (T)Activator.CreateInstance(typeof(T), configPath, logger);
                return config;
            }
        }
        else
        {
            config = (T)Activator.CreateInstance(typeof(T), configPath, logger);
            config.Save();
            return config;
        }
    }

    private static void CreateDirectory(string configPath)
    {
        string directory = Path.GetDirectoryName(configPath);

        if (Directory.Exists(directory) == false)
        {
            Directory.CreateDirectory(directory);
        }
    }

    /// <summary>
    /// The object creation method.  By default uses reflection.
    /// </summary>
    /// <param name="configPath"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    protected virtual T CreateInstance(string configPath, Logger logger)
    {
        return (T)Activator.CreateInstance(typeof(T), configPath, logger);
    }

    public virtual void Save()
    {
        try
        {
            string json = JsonConvert.SerializeObject(this, SerializerSettings);
            File.WriteAllText(ConfigPath, json);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex);
        }
    }
}