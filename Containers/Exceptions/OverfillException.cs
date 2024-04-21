using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Exceptions
{
    public class OverfillException : Exception
    {
        public OverfillException(string message) : base(message)
        {
        }
    }
}
