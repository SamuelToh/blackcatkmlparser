using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackCat
{
    public interface IResourceWriter
    {
         
        //Returns a StreamWriter that can write data to the location specified by outputPath.
        //Pre: outputPath is not the empty string
        //Post: A StreamWtiter object that can write to outputPath has been returned.
        StreamWriter getWriter(String outputPath);
    }
}
