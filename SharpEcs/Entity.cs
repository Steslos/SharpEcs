namespace SharpEcs
{
    public sealed class Entity
    {
        public const int MaximumEntities = 8000;
        internal Signature Signature => new Signature();
    }
}