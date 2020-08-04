using System.Collections.Generic;
using System.Diagnostics;

namespace Steslos.SharpEcs
{
    internal sealed class EntityManager
    {
        private readonly Queue<Entity> availableEntities = new Queue<Entity>();

        public EntityManager()
        {
            for (var i = 0; i < Entity.MaximumEntities; i++)
            {
                availableEntities.Enqueue(new Entity(i));
            }
        }

        public Entity CreateEntity()
        {
            Debug.Assert(availableEntities.Count > 0, "Too many entities used at once.");
            return availableEntities.Dequeue();
        }

        public void DestroyEntity(Entity entity)
        {
            availableEntities.Enqueue(entity);
        }
    }
}