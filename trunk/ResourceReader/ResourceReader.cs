using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackCat
{
    public class ResourceReader
    {
        //Constructor
        ResourceReader() { }

        //Returns a StreamWriter suitable for reading the data from fileURL.
        //Pre: fileURL is not the empty string
        //Post: Returns a StreamReader object that will read from fileURL
        public StreamReader getFileDataReaderObj(String fileURL) 
        {
            return new StreamReader("");
        }
    }
}
