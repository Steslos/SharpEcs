namespace Steslos.SharpEcs
{
    /// <summary>
    /// A signature that contains bits that reference components registered in the SharpECS system.
    /// </summary>
    public sealed class Signature
    {
        internal const int MaximumSignatures = 64;

        private long bitSignature = 0;

        /// <summary>
        /// Create a new blank signature instance.
        /// </summary>
        public Signature()
        {
        }

        /// <summary>
        /// Create a signature instance with the given bits set.
        /// </summary>
        /// <param name="initialBitSet"></param>
        internal Signature(int initialBitSet)
        {
            EnableBit(initialBitSet);
        }

        /// <summary>
        /// Adds the given signature to this one.
        /// </summary>
        /// <param name="signature"></param>
        public void AddSignature(Signature signature)
        {
            bitSignature |= signature.bitSignature;
        }

        /// <summary>
        /// Checks if the given signature is contained in this one.
        /// </summary>
        /// <param name="signature">The signature to check if it is present in this signature.</param>
        /// <returns>True if the given signature is contained in this signature, false if not.</returns>
        public bool ContainsSignature(Signature signature)
        {
            if ((bitSignature & signature.bitSignature) == signature.bitSignature)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Enables a bit in the given position.
        /// </summary>
        /// <param name="bitPosition">The bit position to enable, ie, 0 for bit 1, 1 for bit 2, etc.</param>
        public void EnableBit(int bitPosition)
        {
            bitSignature |= (uint)1 << bitPosition;
        }

        /// <summary>
        /// Disables a bit in the given position.
        /// </summary>
        /// <param name="bitPosition">The bit position to enable, ie, 0 for bit 1, 1 for bit 2, etc.</param>
        public void DisableBit(int bitPosition)
        {
            bitSignature &= ~(uint)(1 << bitPosition);
        }

        /// <summary>
        /// Removes a given signature from this signature, ie, all matching component signatures will be removed from this signature.
        /// </summary>
        /// <param name="signature">The signature to remove from this signature.</param>
        public void RemoveSignature(Signature signature)
        {
            bitSignature &= ~signature.bitSignature;
        }
    }
}