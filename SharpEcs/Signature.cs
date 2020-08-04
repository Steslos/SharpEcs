namespace Steslos.SharpEcs
{
    public sealed class Signature
    {
        internal const int MaximumSignatures = 64;

        private long bitSignature = 0;

        public Signature()
        {
        }

        internal Signature(int initialBitSet)
        {
            EnableBit(initialBitSet);
        }

        public void AddSignature(Signature signature)
        {
            bitSignature |= signature.bitSignature;
        }

        public bool ContainsSignature(Signature signature)
        {
            if ((bitSignature & signature.bitSignature) == signature.bitSignature)
            {
                return true;
            }
            return false;
        }

        public void EnableBit(int bitPosition)
        {
            bitSignature |= (uint)1 << bitPosition;
        }

        public void DisableBit(int bitPosition)
        {
            bitSignature &= ~(uint)(1 << bitPosition);
        }

        public void RemoveSignature(Signature signature)
        {
            bitSignature &= ~signature.bitSignature;
        }
    }
}