using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackCat
{
    public class ResourceWriter : IResourceWriter
    {

        //Constructor
        ResourceWriter(){}

        //Returns a StreamWriter that can write data to the location specified by outputPath.
        //Pre: outputPath is not the empty string
        //Post: A StreamWtiter object that can write to outputPath has been returned.
        public StreamWriter getWriter(String outputPath)
        {
            return new StreamWriter("");
        }

    }


}
