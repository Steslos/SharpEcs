using System.Collections.Generic;
using System.Diagnostics;

namespace Steslos.SharpEcs
{
    internal sealed class ComponentManager
    {
        private readonly Dictionary<string, IComponentCache> componentCaches = new Dictionary<string, IComponentCache>();
        private readonly Dictionary<string, Signature> componentSignatures = new Dictionary<string, Signature>();
        private int nextComponentSignatureBit = 0;
        private readonly Dictionary<string, object> singletonComponents = new Dictionary<string, object>();

        public void AddComponent<T>(Entity entity, T component)
            where T : class
        {
            var componentName = typeof(T).Name;
            Debug.Assert(componentCaches.ContainsKey(componentName), "Component not registered before use.");
            GetComponentCache<T>().InsertData(entity, component);
        }

        public void AddSingletonComponent<T>(T component)
            where T : class
        {
            var componentName = typeof(T).Name;
            Debug.Assert(singletonComponents.ContainsKey(componentName), "Singleton component not registered before use.");
            Debug.Assert(singletonComponents[componentName] == null, "Singleton component attempted to be overwritten.");
            singletonComponents[componentName] = component;
        }

        public void EntityDestroyed(Entity entity)
        {
            foreach (var componentCachePair in componentCaches)
            {
                componentCachePair.Value.EntityDestroyed(entity);
            }
        }

        public T GetComponent<T>(Entity entity)
            where T : class
        {
            var componentName = typeof(T).Name;
            Debug.Assert(componentCaches.ContainsKey(componentName), "Component not registered before use.");
            return GetComponentCache<T>().GetData(entity);
        }

        public Signature GetComponentSignature<T>()
            where T : class
        {
            var componentName = typeof(T).Name;
            Debug.Assert(componentSignatures.ContainsKey(componentName), "Component not registered before use.");
            return componentSignatures[componentName];
        }

        public T GetSingletonComponent<T>()
            where T : class
        {
            var componentName = typeof(T).Name;
            Debug.Assert(singletonComponents.ContainsKey(componentName), "Singleton component not registered before use.");
            return (T)singletonComponents[componentName];
        }

        public void RegisterComponent<T>()
            where T : class
        {
            var componentName = typeof(T).Name;
            Debug.Assert(!componentSignatures.ContainsKey(componentName), "Component already registered.");
            Debug.Assert(!singletonComponents.ContainsKey(componentName), "Component already registered as a singleton.");
            componentCaches.Add(componentName, new ComponentCache<T>());
            componentSignatures.Add(componentName, new Signature(nextComponentSignatureBit));
            nextComponentSignatureBit++;
        }

        public void RegisterSingletonComponent<T>()
            where T : class
        {
            var componentName = typeof(T).Name;
            Debug.Assert(!singletonComponents.ContainsKey(componentName), "Singleton component already registered.");
            Debug.Assert(!componentSignatures.ContainsKey(componentName), "Singleton component already registered as a non-singleton.");
            singletonComponents.Add(componentName, null);
        }

        public void RemoveComponent<T>(Entity entity)
            where T : class
        {
            var componentName = typeof(T).Name;
            Debug.Assert(componentCaches.ContainsKey(componentName), "Component not registered before use.");
            GetComponentCache<T>().RemoveData(entity);
        }

        public void RemoveSingletonComponent<T>()
            where T : class
        {
            var componentName = typeof(T).Name;
            singletonComponents.Remove(componentName);
        }

        private ComponentCache<T> GetComponentCache<T>()
            where T : class
        {
            var componentName = typeof(T).Name;
            Debug.Assert(componentCaches.ContainsKey(componentName), "Component not registered before use.");
            return (ComponentCache<T>)componentCaches[componentName];
        }
    }
}