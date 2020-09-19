using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SharpEcs
{
    internal sealed class ComponentManager
    {
        private readonly Dictionary<string, IComponentCache> componentCaches = new Dictionary<string, IComponentCache>();
        private long nextComponentTypeBit = 0;

        public void EntityDestroyed(Entity entity)
        {
            foreach (var componentCachePair in componentCaches)
            {
                componentCachePair.Value.EntityDestroyed(entity);
            }
        }

        public void RegisterComponent<T>()
            where T : new()
        {
            var componentName = typeof(T).Name;
            Debug.Assert(!componentCaches.ContainsKey(componentName), "Registering component more than once.");

        }

        private ComponentCache<T> GetComponentCache<T>()
        {
            var componentName = typeof(T).Name;
            Debug.Assert(componentCaches.ContainsKey(componentName), "Component not registered before use.");
            return (ComponentCache<T>)componentCaches[componentName];
        }
    }
}