using Unity.Entities;
using Newtonsoft.Json;

public struct Port : IComponentData
{
    [JsonProperty("Port")]
    public int Value;
} 