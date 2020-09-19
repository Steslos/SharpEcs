using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEcs
{
    internal sealed class Signature
    {
        private long bitSignature = 0;

        public void ResetSignature()
            => bitSignature = 0;
    }
}