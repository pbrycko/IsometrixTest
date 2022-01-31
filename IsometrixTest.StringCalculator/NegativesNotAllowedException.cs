using System;
using System.Collections.Generic;

namespace IsometrixTest.StringCalculator
{
    [Serializable]
    public class NegativesNotAllowedException : Exception
    {
        public NegativesNotAllowedException(IEnumerable<int> negatives)
            : base(string.Join(", ", negatives)) { }
    }
}
