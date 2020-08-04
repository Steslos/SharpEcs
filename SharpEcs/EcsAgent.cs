namespace Steslos.SharpEcs
{
    /// <summary>
    /// Contact point for the application using SharpECS.
    /// Allows creation of entities, registering components and systems, and setting components to entities and system signatures.
    /// This is a static class and supports only one ECS world.
    /// </summary>
    public static class EcsAgent
    {
        private static readonly ComponentManager componentManager = new ComponentManager();
        private static readonly EntityManager entityManager = new EntityManager();
        private static readonly SystemManager systemManager = new SystemManager();

        /// <summary>
        /// Adds a component to an entity.
        /// </summary>
        /// <typeparam name="T">The component type (must be registered before using).</typeparam>
        /// <param name="entity">The entity to attach the component to.</param>
        /// <param name="component">The component that will be attached to the component.</param>
        public static void AddComponent<T>(Entity entity, T component)
            where T : class
        {
            componentManager.AddComponent(entity, component);
            entity.Signature.AddSignature(componentManager.GetComponentSignature<T>());
            systemManager.EntitySignatureChanged(entity);
        }

        /// <summary>
        /// Adds a component that can only have one instance in SharpECS.
        /// This is best used for system-wide states, such as render or asset states.
        /// </summary>
        /// <typeparam name="T">The type of the singleton component class (must be registered before use).</typeparam>
        /// <param name="component">The component that will be added to SharpECS as a singleton.</param>
        public static void AddSingletonComponent<T>(T component)
            where T : class
        {
            componentManager.AddSingletonComponent<T>(component);
        }

        /// <summary>
        /// Adds the component to a signature.
        /// This is useful for setting what entities a system is interested in working with.
        /// </summary>
        /// <typeparam name="T">The type of the component (must be registered before use).</typeparam>
        /// <param name="signature">The signature to add the component's signature to.</param>
        public static void AddComponentSignature<T>(Signature signature)
            where T : class
        {
            signature.AddSignature(componentManager.GetComponentSignature<T>());
        }

        /// <summary>
        /// Returns an available entity.
        /// There is a finite amount of entities available.
        /// </summary>
        /// <returns>An available entity. Call <see cref="DestroyEntity"/> when done with it.</returns>
        public static Entity CreateEntity()
        {
            return entityManager.CreateEntity();
        }

        /// <summary>
        /// Destroys an entity previous returned from <see cref="CreateEntity"/>.
        /// There is a finite amount of entities available.
        /// </summary>
        /// <param name="entity">The entity to return back to the pool of available entities.</param>
        public static void DestroyEntity(Entity entity)
        {
            systemManager.EntityDestroyed(entity);
            componentManager.EntityDestroyed(entity);
            entityManager.DestroyEntity(entity);
        }

        /// <summary>
        /// Gets a component that has previously been attached to an entity.
        /// </summary>
        /// <typeparam name="T">The component type that was added to the entity.</typeparam>
        /// <param name="entity">The entity with the added component.</param>
        /// <returns>The component that was attached to the entity.</returns>
        public static T GetComponent<T>(Entity entity)
            where T : class
        {
            return componentManager.GetComponent<T>(entity);
        }

        /// <summary>
        /// Gets a component that was previously registered and added as a singleton component.
        /// </summary>
        /// <typeparam name="T">The singleton component type.</typeparam>
        /// <returns>The singleton component.</returns>
        public static T GetSingletonComponent<T>()
            where T : class
        {
            return componentManager.GetSingletonComponent<T>();
        }

        /// <summary>
        /// Register a component type, so in the future it may be added to entities with <see cref="AddComponent"/>.
        /// </summary>
        /// <typeparam name="T">The component type to register</typeparam>
        public static void RegisterComponent<T>()
            where T : class
        {
            componentManager.RegisterComponent<T>();
        }

        /// <summary>
        /// Register a component type as a singleton, so in the future it may be retrieved with <see cref="GetSingletonComponent"/>.
        /// You must call <see cref="AddSingletonComponent"/> to actually add the singleton component instance after registering it.
        /// </summary>
        /// <typeparam name="T">The component type to register as a singleton.</typeparam>
        public static void RegisterSingletonComponent<T>()
            where T : class
        {
            componentManager.RegisterSingletonComponent<T>();
        }

        /// <summary>
        /// Register a system. Systems are classes that inherit from <see cref="EcsSystem"/> and have a Entities collection that will
        /// contain all entities that match the system's signature as set from <see cref="SetSystemSignature{T}(Signature)"/>.
        /// </summary>
        /// <typeparam name="T">The class type of the system to register.</typeparam>
        /// <returns>The instance of the system registered in the SharpECS system.</returns>
        public static T RegisterSystem<T>()
            where T : EcsSystem, new()
        {
            return systemManager.RegisterSystem<T>();
        }

        /// <summary>
        /// Removes a component from an entity.
        /// </summary>
        /// <typeparam name="T">The type of the component to remove from the entity.</typeparam>
        /// <param name="entity">The entity to remove the component from.</param>
        public static void RemoveComponent<T>(Entity entity)
            where T : class
        {
            componentManager.RemoveComponent<T>(entity);
            entity.Signature.RemoveSignature(componentManager.GetComponentSignature<T>());
            systemManager.EntitySignatureChanged(entity);
        }

        /// <summary>
        /// Removes a component's signature from a given signature.
        /// </summary>
        /// <typeparam name="T">The type of the component to remove from the signature.</typeparam>
        /// <param name="signature">The signature to remove the component's signature from.</param>
        public static void RemoveComponentSignature<T>(Signature signature)
            where T : class
        {
            signature.RemoveSignature(componentManager.GetComponentSignature<T>());
        }

        /// <summary>
        /// Remove a component previously registered as a singleton.
        /// </summary>
        /// <typeparam name="T">The type of the component registered as a singleton.</typeparam>
        public static void RemoveSingletonComponent<T>()
            where T : class
        {
            componentManager.RemoveSingletonComponent<T>();
        }

        /// <summary>
        /// Sets a system in the SharpECS system to have the given signature.
        /// This will ensure the system's Entities property will be populated with entities that match (at minimum) this signature.
        /// </summary>
        /// <typeparam name="T">The class type of the EcsSystem.</typeparam>
        /// <param name="signature">The signature to give the EcsSystem.</param>
        public static void SetSystemSignature<T>(Signature signature)
            where T : EcsSystem
        {
            systemManager.SetSystemSignature<T>(signature);
        }
    }
}