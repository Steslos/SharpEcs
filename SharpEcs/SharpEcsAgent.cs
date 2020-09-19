namespace SharpEcs
{
    public static class SharpEcsAgent
    {
        private static readonly ComponentManager componentManager = new ComponentManager();
        private static readonly EntityManager entityManager = new EntityManager();
        private static readonly SystemManager systemManager = new SystemManager();

        public static void AddComponent<T>(Entity entity, T component)
        {
            componentManager.AddComponent<T>(entity, component);
            var componentSignature = componentManager.GetComponentSignature<T>();
            entity.Signature.AddSignature(componentSignature);
            systemManager.EntitySignatureChanged(entity);
        }

        public static Entity CreateEntity()
            => entityManager.CreateEntity();

        public static void DestroyEntity(Entity entity)
        {
            componentManager.EntityDestroyed(entity);
            systemManager.EntityDestroyed(entity);
            entityManager.DestroyEntity(entity);
        }

        public static T GetComponent<T>(Entity entity)
            => componentManager.GetComponent<T>(entity);

        public static Signature GetComponentSignature<T>()
            => componentManager.GetComponentSignature<T>();

        public static void RegisterComponent<T>()
            => componentManager.RegisterComponent<T>();

        public static T RegisterSystem<T>()
            where T : SharpEcsSystem, new()
            => systemManager.RegisterSystem<T>();

        public static void RemoveComponent<T>(Entity entity)
        {
            componentManager.RemoveComponent<T>(entity);
            var componentSignature = componentManager.GetComponentSignature<T>();
            entity.Signature.RemoveSignature(componentSignature);
            systemManager.EntitySignatureChanged(entity);
        }

        public static void SetSystemSignature<T>(Signature signature)
            where T : SharpEcsSystem
            => systemManager.SetSystemSignature<T>(signature);
    }
}