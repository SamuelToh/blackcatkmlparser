using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackCat
{
    interface IResourceReader
    {
        //Constructor
        // public ResourceReader(String fileURL);

        //Returns a StreamReader suitable for reading the data from fileURL.
        //Pre: fileURL is not the empty string
        //Post: Returns a StreamReader object that will read from fileURL

        StreamReader getFileDataReaderObj();

        // Is this really the best way to do this? We appear to be going to a lot of trouble
        // to call a StreamReader object. Couldn't we just do that directly from the data model
        // class?

        // I think the broader purpose of this class is probably to get hold of data from other 
        // types of sources in the future such as databases. In that case, maybe we should code 
        // the functionality we would like into the resource reader, e.g. have a method readLine
        // that is called on the ResourceReader rather than on the StreamReader that lies underneath.
        // Then this class can simply call the StreamReader method, i.e. it's a wrapper method.

        // Something like:

        // Closes the ResourceReader object and the underlying stream and releases any system resources 
        // associated with it.
        // Pre: True
        // Post: The ResourceReader has been closed and its resources returned to the system.

        // void close();

        // Returns an integer representing the next available character but does not consume it. 
        // If no remaining characters, returns -1.
        // Pre: True.
        // Post: An integer representing the next character has been returned, but the character 
        // remains available for future consumption.

        // int peek();

        // Reads the next character from the input stream and consumes the character.
        // Returns -1 if there is no next character.
        // Pre: True.
        // Post: An integer representing the next character has been returned and the
        // character has been consumed.

        // int read();

        // Reads a line of characters from the current stream and returns the data as a string.
        // Returns null if no more lines of data are left in the file.
        // Pre: True.
        // Post: A string representing the next line of data is returned, or the empty string if
        // there is no further data available. The line of data is consumed.

        // string readLine();

        // Reads the stream from the current position to the end of the stream and returns
        // a string corresponding to this portion of the stream. Returns the null string if
        // we are already at the end of the stream.
        // Pre: True
        // Post: A string representing the rest of the stream is returned, or the empty string if
        // there is no further data available. The remainder of the stream is consumed.

        // string readToEnd();
    }
}
