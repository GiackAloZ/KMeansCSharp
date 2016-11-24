using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans.Exceptions
{
	public class NotConvergentException : KMeansException 
	{
		public NotConvergentException() : base() { }

		public NotConvergentException(string message) : base(message) { }

		public NotConvergentException(string message, Exception inner) : base (message, inner) { }
	}
}
