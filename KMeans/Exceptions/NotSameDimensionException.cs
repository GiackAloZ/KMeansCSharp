using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans.Exceptions
{
    public class NotSameDimensionException : KMeansException
    {
        public NotSameDimensionException() : base() { }

        public NotSameDimensionException(string message) : base(message) { }

        public NotSameDimensionException(string message, Exception inner) : base (message, inner) { }
    }
}
