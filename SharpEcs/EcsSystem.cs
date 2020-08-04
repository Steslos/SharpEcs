using System.Collections.Generic;

namespace Steslos.SharpEcs
{
    /// <summary>
    /// Allows a system class to have a list of entities that are of interest to the system to iterate over, based on the system signature.
    /// </summary>
    public abstract class EcsSystem
    {
        /// <summary>
        /// A hash set of all entities that (at minimum) match the system signature.
        /// </summary>
        public readonly HashSet<Entity> Entities = new HashSet<Entity>();

        internal Signature Signature = new Signature();
    }
}