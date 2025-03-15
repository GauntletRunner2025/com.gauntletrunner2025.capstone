using Unity.Entities;
using Unity.Collections;
using UnityEngine;
using Newtonsoft.Json;

public struct NeedsPortParsing : IComponentData { }

public partial class PortParser : ConsensusSystem
{
    struct Flag : IComponentData { }
    public override ComponentType SystemFlag => typeof(Flag);

    public override EntityQuery InterestingEntitiesSignature => Query;
    EntityQuery Query;

    public override void AnalyzeEntity(EntityManager em, Entity entity, out bool isComplete)
    {
        isComplete = false;
        var rawJson = em.GetComponentData<RawJSON>(entity);
        try
        {
            var port = ParsePort(rawJson.Value);
            EntityManager.AddComponentData(entity, port);
            Debug.Log($"Successfully parsed port {port.Value} from config");
            isComplete = true;
        }
        catch (JsonException e)
        {
            Debug.LogError($"Failed to parse Port from config JSON: {e.Message}");
        }
    }

    public override void SetupQuery(EntityManager em)
    {
        Query = em.CreateEntityQuery(
            ComponentType.ReadOnly<RawJSON>(),
            ComponentType.ReadOnly<NeedsPortParsing>(),
            ComponentType.Exclude<Port>());
    }

    public static Port ParsePort(string jsonContent)
    {
        if (string.IsNullOrEmpty(jsonContent))
        {
            throw new JsonException("JSON content is null or empty");
        }

        try
        {
            var port = JsonConvert.DeserializeObject<Port>(jsonContent);
            if (port.Value == 0)
            {
                Debug.LogWarning("Port value is 0 (default). This may indicate a configuration issue.");
            }
            return port;
        }
        catch (JsonException e)
        {
            throw new JsonException($"Failed to parse Port from JSON: {e.Message}", e);
        }
    }

}