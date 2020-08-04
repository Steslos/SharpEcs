namespace Steslos.SharpEcs
{
    public static class EcsAgent
    {
        private static readonly ComponentManager componentManager = new ComponentManager();
        private static readonly EntityManager entityManager = new EntityManager();
        private static readonly SystemManager systemManager = new SystemManager();

        public static void AddComponent<T>(Entity entity, T component)
            where T : class
        {
            componentManager.AddComponent(entity, component);
            entity.Signature.AddSignature(componentManager.GetComponentSignature<T>());
            systemManager.EntitySignatureChanged(entity);
        }

        public static void AddSingletonComponent<T>(T component)
            where T : class
        {
            componentManager.AddSingletonComponent<T>(component);
        }

        public static void AddComponentSignature<T>(Signature signature)
            where T : class
        {
            signature.AddSignature(componentManager.GetComponentSignature<T>());
        }

        public static Entity CreateEntity()
        {
            return entityManager.CreateEntity();
        }

        public static void DestroyEntity(Entity entity)
        {
            systemManager.EntityDestroyed(entity);
            componentManager.EntityDestroyed(entity);
            entityManager.DestroyEntity(entity);
        }

        public static T GetComponent<T>(Entity entity)
            where T : class
        {
            return componentManager.GetComponent<T>(entity);
        }

        public static T GetSingletonComponent<T>()
            where T : class
        {
            return componentManager.GetSingletonComponent<T>();
        }

        public static void RegisterComponent<T>()
            where T : class
        {
            componentManager.RegisterComponent<T>();
        }

        public static void RegisterSingletonComponent<T>()
            where T : class
        {
            componentManager.RegisterSingletonComponent<T>();
        }

        public static T RegisterSystem<T>()
            where T : EcsSystem, new()
        {
            return systemManager.RegisterSystem<T>();
        }

        public static void RemoveComponent<T>(Entity entity)
            where T : class
        {
            componentManager.RemoveComponent<T>(entity);
            entity.Signature.RemoveSignature(componentManager.GetComponentSignature<T>());
            systemManager.EntitySignatureChanged(entity);
        }

        public static void RemoveComponentSignature<T>(Signature signature)
            where T : class
        {
            signature.RemoveSignature(componentManager.GetComponentSignature<T>());
        }

        public static void RemoveSingletonComponent<T>()
            where T : class
        {
            componentManager.RemoveSingletonComponent<T>();
        }

        public static void SetSystemSignature<T>(Signature signature)
            where T : EcsSystem
        {
            systemManager.SetSystemSignature<T>(signature);
        }
    }
}