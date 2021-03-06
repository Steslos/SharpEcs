﻿using System.Collections.Generic;

namespace SharpEcs
{
    public abstract class SharpEcsSystem
    {
        public readonly HashSet<Entity> Entities = new HashSet<Entity>();

        internal Signature Signature { get; } = new Signature();
    }
}