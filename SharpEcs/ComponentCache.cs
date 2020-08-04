using System.Diagnostics;

namespace Steslos.SharpEcs
{
    internal sealed class ComponentCache<T> : IComponentCache
        where T : class
    {
        private T[] componentCache = new T[Entity.MaximumEntities];

        public ComponentCache()
        {
            for (var i = 0; i < componentCache.Length; i++)
            {
                componentCache[i] = null;
            }
        }

        public void EntityDestroyed(Entity entity)
        {
            if (componentCache[entity.Id] != null)
            {
                RemoveData(entity);
            }
        }

        public T GetData(Entity entity)
        {
            Debug.Assert(componentCache[entity.Id] != null, "Retrieving non-existant component.");
            return componentCache[entity.Id];
        }

        public void InsertData(Entity entity, T component)
        {
            Debug.Assert(componentCache[entity.Id] == null, "Component added to the same entity more than once.");
            componentCache[entity.Id] = component;
        }

        public void RemoveData(Entity entity)
        {
            Debug.Assert(componentCache[entity.Id] != null, "Removing non-existant component.");
            componentCache[entity.Id] = null;
        }
    }
}