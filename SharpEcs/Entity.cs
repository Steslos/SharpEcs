namespace Steslos.SharpEcs
{
    /// <summary>
    /// An entity that will have components added to it to represent an object.
    /// It will appear in systems whose signature matches (at minimum) the components attached to this entity.
    /// Creating and destroying entities is performed by <see cref="EcsAgent"/> methods.
    /// </summary>
    public sealed class Entity
    {
        internal const int MaximumEntities = 5000;

        internal readonly int Id;
        internal Signature Signature = new Signature();

        internal Entity(int id)
        {
            Id = id;
        }
    }
}