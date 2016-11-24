
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class KMeansException : Exception
    {
        public KMeansException() : base() { }

        public KMeansException(string message) : base(message) { }

        public KMeansException(string message, Exception inner) : base (message, inner) { }
    }
}
