using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BlackCat
{
    public interface IKMLParserControl
    {
        // Creates the KML file using the files previously supplied by the user, writing it to the 
        // location desired and arranging the updating of a progress bar as the operation proceeds. 
        // Returns an integer denoting success (0) or failure (1).

        // Pre: outputFileURL is not the empty string and progressBar is not null.
        // Post: A new KML file is written to outputFolderURL, created from the input files.

        int GenerateKMLFile(String outputFileURL, ProgressBar progressBar); 
        
       
        string[] GetMapInfoDataFields();


        // Loads the KML file fileURL into the system, arranging the updating of a progress bar as the 
        // operation proceeds. Returns an integer denoting the result of the operation as follows:

        // value = 0 (No problems) 
        // value = 1 (file doesn’t exist)
        // value = 2 (file is not readable)
        // value = 3 (file is in the wrong format).

        // Pre: fileURL is not the empty string and progressBar is not null.
        // Post: fileURL has been loaded and an integer denoting success or otherwise has been returned.

        int LoadKML(String fileURL, ProgressBar progressBar);

        // Loads the MapInfo files midFileURL and mifFileURL into the system, arranging the updating of 
        // a progress bar as the operation proceeds. Returns an integer denoting the result of the 
        // operation as follows:

        // value = 0 (No problems) 
        // value = 1 (mid file doesn’t exist)
        // value = 2 (mid file is not readable)
        // value = 3 (mid file is in the wrong format)
        // value = 4 (mif file doesn’t exist)
        // value = 5 (mif file is not readable)
        // value = 6 (mif file is in the wrong format).

        // Pre: midFileURL, mifFileURL are not empty strings and progressBar is not null.
        // Post: midFileURL and mifFileURL have been loaded and an integer denoting success or otherwise 
        // has been returned.

        int LoadMapInfo(String midFileURL, String mifFileURL ,ProgressBar progressBar);

    }
}
