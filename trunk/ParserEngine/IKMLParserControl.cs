using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BlackCat
{
    interface IKMLParserControl
    {
        // Constructor is protected. It cannot be called from outside the class. Use Instance instead.

        // Special property needed to implement the Singleton design pattern. Either instantiates
        // the class if this has not occurred yet or returns a reference to the class if 
        // instantiation has occurred. Meaning there is never more than one instance of this class
        // at any time.

        // static KMLParserControl Instance();

        // Returns a boolean indicating whether the geographic and sociological data fields can 
        // be linked using the fields geoField and socField.

        // Pre: geoField is not the empty string and socField is not the empty string. 
        // Post: A boolean indicating whether the desired linking operation can occur has been returned.

        bool canLink(String geoField, String socField);

        // Creates the KML file using the files previously supplied by the user, writing it to the 
        // location desired and arranging the updating of a progress bar as the operation proceeds. 
        // Returns an integer denoting success (0) or failure (1).

        // Pre: outputFileURL is not the empty string and progressBar is not null.
        // Post: A new KML file is written to outputFolderURL, created from the input files.

        int generateKMLFile(String outputFileURL, ProgressBar progressBar); 
        
        // Returns a list of the data fields in the geographical data file(s) that could be used to perform 
        // data linking on.

        // Pre: A geographical data set has been loaded into the system 
        // Post: A list of the data fields present in the geographical data set has been returned.

        List<string> getGeographicalDataFields();

        // Returns a list of the data fields in the sociological data file that could be used to 
        // perform data linking on.

        // Pre: A sociological data file has been loaded into the system
        // Post: A list of the data fields present in the sociological data file has been returned.

        List<string> getSociologicalDataFields();

        // Loads the Excel file fileURL into the system, arranging the updating of a progress bar as the 
        // operation proceeds. Returns an integer denoting the result of the operation as follows:

        // value = 0 (No problems) 
        // value = 1 (file doesn’t exist)
        // value = 2 (file is not readable)
        // value = 3 (file is in the wrong format).

        // Pre: fileURL is not the empty string and progressBar is not null.
        // Post: fileURL have been loaded and an integer denoting success or otherwise has been returned.

        int loadExcel(String fileURL, ProgressBar progressBar);

        // Loads the KML file fileURL into the system, arranging the updating of a progress bar as the 
        // operation proceeds. Returns an integer denoting the result of the operation as follows:

        // value = 0 (No problems) 
        // value = 1 (file doesn’t exist)
        // value = 2 (file is not readable)
        // value = 3 (file is in the wrong format).

        // Pre: fileURL is not the empty string and progressBar is not null.
        // Post: fileURL has been loaded and an integer denoting success or otherwise has been returned.

        int loadKML(String fileURL, ProgressBar progressBar);

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

        int loadMapInfo(String midFileURL, String mifFileURL ,ProgressBar progressBar);

        // Requests validation of a folder to ensure it exists, can be written to etc. Returns an 
        // integer denoting the result of the verification as follows:

        // value = 0 (Folder is OK) 
        // value = 1 (folder doesn’t exist)
        // value = 2 (folder is not writable for the current user)
        // value = 3 (folder doesn’t have enough room)
        // value = 4 (proposed path is too long for .NET to handle).

        // Pre: folderURL is the full path and not the empty string.
        // Post: The status of the folder has been verified and an integer denoting the folder’s status 
        // has been returned.

        int validateFolder(string folderURL);
    }
}
