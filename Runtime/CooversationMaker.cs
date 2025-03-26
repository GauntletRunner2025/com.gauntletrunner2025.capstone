using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public partial class ConversationMaker : SystemBase {
    protected override void OnCreate() {
        //Always create a fresh conversation, ready to start.
        //This is how new conversations are made
        var entity = EntityManager.CreateEntity();
        EntityManager.AddComponentData(entity, new Conversation());
    }

    protected override void OnStartRunning() {

    }

    protected override void OnUpdate() {
        using EntityCommandBuffer ecb  = new EntityCommandBuffer(Allocator.TempJob);
        foreach(var (item, entity) in SystemAPI
            .Query<Response>()
            .WithAll<Complete>()
            .WithNone<Conversation>()
            .WithEntityAccess()){
               //Debug.Log($"[{this.GetType().Name}] {entity}");
               //Put a conversation on the Response
                ecb.AddComponent(entity, new Conversation());
            
            }

        ecb.Playback(EntityManager);

        }
}