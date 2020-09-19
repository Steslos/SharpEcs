using System.Collections.Generic;
using System.Diagnostics;

namespace SharpEcs
{
    internal sealed class SystemManager
    {
        private readonly Dictionary<string, SharpEcsSystem> systems = new Dictionary<string, SharpEcsSystem>();

        internal void EntityDestroyed(Entity entity)
        {
            foreach (var systemPair in systems)
            {
                systemPair.Value.Entities.Remove(entity);
            }
        }

        internal void EntitySignatureChanged(Entity entity)
        {
            foreach (var systemPair in systems)
            {
                var system = systemPair.Value;
                if (entity.Signature.MatchesSignature(system.Signature))
                {
                    system.Entities.Add(entity);
                }
                else
                {
                    system.Entities.Remove(entity);
                }
            }
        }

        internal void SetSystemSignature<T>(Signature signature)
        {
            var systemName = typeof(T).Name;
            Debug.Assert(systems.ContainsKey(systemName), "System used before registering.");
            systems[systemName].Signature.SetSignature(signature);
        }

        internal SharpEcsSystem RegisterSystem<T>()
            where T : SharpEcsSystem, new()
        {
            var systemName = typeof(T).Name;
            Debug.Assert(!systems.ContainsKey(systemName), "Registering system more than once.");
            var system = new T();
            systems.Add(systemName, system);
            return system;
        }
    }
}