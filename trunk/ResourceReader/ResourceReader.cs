using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackCat
{
    public class ResourceReader : IResourceReader
    {
        private StreamReader reader;

        //Constructor

        public ResourceReader(String fileURL)
        {
            reader = new StreamReader(fileURL);
        }

        //Returns a StreamReader suitable for reading the data from fileURL.
        //Pre: fileURL is not the empty string
        //Post: Returns a StreamReader object that will read from fileURL

        public StreamReader getFileDataReaderObj()
        {
            return reader;
        }

        // Closes the ResourceReader object and the underlying stream and releases any system resources 
        // associated with it.
        // Pre: True
        // Post: The ResourceReader has been closed and its resources returned to the system.

        // public void close()
        // {
        //    reader.Close();
        // }

        // Returns an integer representing the next available character but does not consume it. 
        // If no remaining characters, returns -1.
        // Pre: True.
        // Post: An integer representing the next character has been returned, but the character 
        // remains available for future consumption.

        // public int peek()
        // {
        //     return reader.Peek();
        // }

        // Reads the next character from the input stream and consumes the character.
        // Returns -1 if there is no next character.
        // Pre: True.
        // Post: An integer representing the next character has been returned and the
        // character has been consumed.

        // public int read()
        // {
        //     return reader.Read();
        // }

        // Reads a line of characters from the current stream and returns the data as a string.
        // Returns null if no more lines of data are left in the file.
        // Pre: True.
        // Post: A string representing the next line of data is returned, or the empty string if
        // there is no further data available. The line of data is consumed.

        // public string readLine()
        // {
        //    return reader.ReadLine();
        //}

        // Reads the stream from the current position to the end of the stream and returns
        // a string corresponding to this portion of the stream. Returns the null string if
        // we are already at the end of the stream.
        // Pre: True
        // Post: A string representing the rest of the stream is returned, or the empty string if
        // there is no further data available. The remainder of the stream is consumed.

        // public string readToEnd()
        // {
        //    return reader.ReadToEnd();
        //}
    }
}
