using System.Collections.Generic;
using System.Diagnostics;

namespace SharpEcs
{
    internal sealed class EntityManager
    {
        private readonly Queue<Entity> availableEntities = new Queue<Entity>();

        public EntityManager()
        {
            for (var i = 0; i < Entity.MaximumEntities; i++)
            {
                availableEntities.Enqueue(new Entity());
            }
        }

        public Entity CreateEntity()
        {
            Debug.Assert(availableEntities.Count > 0, "Exhaused available entities.");
            return availableEntities.Dequeue();
        }

        public void DestroyEntity(Entity entity)
        {
            entity.Signature.ResetSignature();
            availableEntities.Enqueue(entity);
        }
    }
}