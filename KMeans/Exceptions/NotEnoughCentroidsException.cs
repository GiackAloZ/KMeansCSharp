using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans.Exceptions
{
    public class NotEnoughCentroidsException : KMeansException
    {
        public NotEnoughCentroidsException() : base() { }

        public NotEnoughCentroidsException(string message) : base(message) { }

        public NotEnoughCentroidsException(string message, Exception inner) : base (message, inner) { }
    }
}
