namespace SharpEcs
{
    public sealed class Signature
    {
        internal long BitSignature { get; private set; } = 0;

        public void AddSignature(Signature signature)
            => BitSignature |= signature.BitSignature;

        public void RemoveSignature(Signature signature)
            => BitSignature &= ~signature.BitSignature;

        internal void DisableBit(int bitPosition)
            => BitSignature &= ~(uint)1 << bitPosition;

        internal void EnableBit(int bitPosition)
            => BitSignature |= (uint)1 << bitPosition;

        internal bool MatchesSignature(Signature signature)
            => ((BitSignature & signature.BitSignature) == signature.BitSignature);

        internal void ResetSignature()
            => BitSignature = 0;
    }
}