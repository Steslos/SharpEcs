namespace SharpEcs
{
    internal sealed class Signature
    {
        internal long BitSignature { get; private set; } = 0;
        
        public bool MatchesSignature(Signature signature)
            => ((BitSignature & signature.BitSignature) == signature.BitSignature);

        public void ResetSignature()
            => BitSignature = 0;

        public void SetBit(long bitPosition)
            => BitSignature |= 1 << bitPosition;

        public void SetSignature(Signature signature)
            => BitSignature = signature.BitSignature;
    }
}