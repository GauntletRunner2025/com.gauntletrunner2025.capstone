using Unity.Entities;
using UnityEngine;
using System.IO;

namespace WindsurfRules
{
    public partial class ConfigLoader : SystemBase
    {
        protected override void OnStartRunning()
        {
            // Only create config entity if it doesn't exist
            if (!SystemAPI.HasSingleton<Config>())
            {
                string configPath = Path.Combine(Application.dataPath, "config.json");
                if (!File.Exists(configPath))
                {
                    Debug.LogError($"Config file not found at {configPath}");
                    return;
                }

                string jsonContent;
                try
                {
                    jsonContent = File.ReadAllText(configPath);
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Failed to read config file: {e.Message}");
                    return;
                }

                Entity configEntity = EntityManager.CreateEntity();
                EntityManager.AddComponentData(configEntity, new Config());
                EntityManager.AddComponentData(configEntity, new RawJSON { Value = jsonContent });
            }
        }

        protected override void OnUpdate() { }
    }
}
