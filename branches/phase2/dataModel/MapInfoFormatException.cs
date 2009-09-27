using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    class MapInfoFormatException : Exception
    {
        public MapInfoFormatException(string message)
            : base(message) 
        {
        }

        public MapInfoFormatException(string message, Exception innerEx)
            : base(message, innerEx) 
        { 
        }
    }
}
