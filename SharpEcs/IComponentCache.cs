﻿namespace SharpEcs
{
    internal interface IComponentCache
    {
        void EntityDestroyed(Entity entity);
    }
}