using System.Collections.Generic;
using System.Diagnostics;

namespace Steslos.SharpEcs
{
    internal sealed class SystemManager
    {
        private readonly Dictionary<string, EcsSystem> systems = new Dictionary<string, EcsSystem>();

        public void EntityDestroyed(Entity entity)
        {
            foreach (var systemPair in systems)
            {
                var system = systemPair.Value;
                system.Entities.Remove(entity);
            }
        }

        public void EntitySignatureChanged(Entity entity)
        {
            foreach (var systemPair in systems)
            {
                var system = systemPair.Value;
                if (entity.Signature.ContainsSignature(system.Signature))
                {
                    system.Entities.Add(entity);
                }
                else
                {
                    system.Entities.Remove(entity);
                }
            }
        }

        public T RegisterSystem<T>()
            where T : EcsSystem, new()
        {
            var systemName = typeof(T).Name;
            Debug.Assert(!systems.ContainsKey(systemName), "Registering system more than once.");
            var newSystem = new T();
            systems.Add(systemName, newSystem);
            return newSystem;
        }

        public void SetSystemSignature<T>(Signature signature)
            where T : EcsSystem
        {
            var systemName = typeof(T).Name;
            Debug.Assert(systems.ContainsKey(systemName), "System used before registered.");
            systems[systemName].Signature = signature;
        }
    }
}