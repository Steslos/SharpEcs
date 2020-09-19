using System.Collections.Generic;
using System.Diagnostics;

namespace SharpEcs
{
    internal sealed class ComponentCache<T> : IComponentCache
    {
        public Signature Signature { get; } = new Signature();

        private readonly Dictionary<Entity, T> entityComponents = new Dictionary<Entity, T>();

        public void EntityDestroyed(Entity entity)
        {
            if (entityComponents.ContainsKey(entity))
            {
                RemoveData(entity);
            }
        }

        public T GetData(Entity entity)
        {
            Debug.Assert(entityComponents.ContainsKey(entity), "Getting component that the entity does not have.");
            return entityComponents[entity];
        }

        public void InsertData(Entity entity, T component)
        {
            Debug.Assert(!entityComponents.ContainsKey(entity), "Adding component to same entity more than once.");
            entityComponents.Add(entity, component);
        }

        public void RemoveData(Entity entity)
        {
            Debug.Assert(entityComponents.ContainsKey(entity), "Removing a component that the entity does not have.");
            entityComponents.Remove(entity);
        }
    }
}