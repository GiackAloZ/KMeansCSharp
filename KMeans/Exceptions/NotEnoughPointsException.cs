using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class NotEnoughPointsException : KMeansException
    {
        public NotEnoughPointsException() : base() { }

        public NotEnoughPointsException(string message) : base(message) { }

        public NotEnoughPointsException(string message, Exception inner) : base (message, inner) { }
    }
}
