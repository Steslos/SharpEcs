using System.Collections.Generic;

namespace Steslos.SharpEcs
{
    public abstract class EcsSystem
    {
        public readonly HashSet<Entity> Entities = new HashSet<Entity>();
        internal Signature Signature = new Signature();
    }
}