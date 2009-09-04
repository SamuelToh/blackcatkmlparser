using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackCat
{
    public interface IResourceReader
    {

        //Returns a StreamWriter suitable for reading the data from fileURL.
        //Pre: fileURL is not the empty string
        //Post: Returns a StreamReader object that will read from fileURL
        StreamReader getFileDataReaderObj(String fileURL);
    }
}
