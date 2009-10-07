using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    //TODO: improve this comment :)
    // Describes an error state that occurs when the .mif and .mid files have non-matching data
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
