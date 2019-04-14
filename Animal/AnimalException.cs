using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal
{
    class AnimalException : Exception
    {
        public AnimalException(string message) : base(message)
        {
            
        }
    }
}
