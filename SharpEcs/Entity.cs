namespace Steslos.SharpEcs
{
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