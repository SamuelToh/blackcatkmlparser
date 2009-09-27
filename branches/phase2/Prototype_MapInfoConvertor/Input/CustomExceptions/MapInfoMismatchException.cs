using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class MapInfoMismatchException : Exception
    {
        public MapInfoMismatchException(string message)
            : base(message) 
        {
        }
        
        public MapInfoMismatchException(string message, Exception innerEx)
            : base(message, innerEx) 
        { 
        }
    }
}
