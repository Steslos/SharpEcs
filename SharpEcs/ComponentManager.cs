using System.Collections.Generic;
using System.Diagnostics;

namespace SharpEcs
{
    internal sealed class ComponentManager
    {
        private readonly Dictionary<string, IComponentCache> componentCaches = new Dictionary<string, IComponentCache>();
        private int nextComponentTypeBit = 0;

        public void AddComponent<T>(Entity entity, T component)
            => GetComponentCache<T>().InsertData(entity, component);

        public void EntityDestroyed(Entity entity)
        {
            foreach (var componentCachePair in componentCaches)
            {
                componentCachePair.Value.EntityDestroyed(entity);
            }
        }

        public T GetComponent<T>(Entity entity)
            => GetComponentCache<T>().GetData(entity);

        public Signature GetComponentSignature<T>()
            => GetComponentCache<T>().Signature;

        public void RegisterComponent<T>()
        {
            Debug.Assert(nextComponentTypeBit < 64, "Too many components registered.");
            var componentName = typeof(T).Name;
            Debug.Assert(!componentCaches.ContainsKey(componentName), "Registering component more than once.");
            var componentCache = new ComponentCache<T>();
            componentCache.Signature.EnableBit(nextComponentTypeBit);
            componentCaches.Add(componentName, componentCache);
            nextComponentTypeBit++;
        }

        public void RemoveComponent<T>(Entity entity)
            => GetComponentCache<T>().RemoveData(entity);

        private ComponentCache<T> GetComponentCache<T>()
        {
            var componentName = typeof(T).Name;
            Debug.Assert(componentCaches.ContainsKey(componentName), "Component not registered before use.");
            return (ComponentCache<T>)componentCaches[componentName];
        }
    }
}