using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BlackCat
{
    public enum SociologicalDataSelection { NONE, WINNING_PARTY, SEAT_SAFETY };

    public interface IKMLParserControl
    {       

        // Checks to see if any sociological data can be added to the geographical data.
        // Returns a boolean – true if sociological data can be added, false if it can’t.
        // Pre: True
        // Post: A boolean has been returned indicating if sociological data can be added to 
        // this set of geographical data.

        bool CanAddSociologicalData();

        // Creates the KML file using the files previously supplied by the user, writing it to the 
        // location desired and arranging the updating of a progress bar as the operation 
        // proceeds. 
        // Returns an integer denoting success (0) or failure (1).
        // Pre: outputFileURL is not the empty string progressBar is not  null
        // Post: A new KML file is written to outputFolderURL, created from the input files.

        int GenerateKMLFile(String outputFileURL, ProgressBar progressBar);

        // Extracts the data fields from the MapInfo file so that the user can select one or more // of them to display in Google Earth.
        // Returns a string array containing the names of the data fields present in the 
        // MapInfo file.
        // Pre: True
        // Post: An array of field names corresponding to those found in the MapInfo file is 
        // returned.

        List<String> GetMapInfoDataFields();

        // Loads the KML file fileURL into the system, arranging the updating of a progress 
        // bar as the operation proceeds. 
        // Returns an integer denoting success (0) or failure (1).
        // Pre: fileURL is not the empty string and progressBar is not null.
        // Post: fileURL has been loaded and an integer denoting success or otherwise has 
        // been returned.

        int LoadKML(String fileURL, ProgressBar progressBar);

        // Loads the MapInfo files midFileURL and mifFileURL into the system, arranging 
        // the updating of a progress bar as the operation proceeds. 
        // Returns an integer denoting success (0) or failure (1).
        // Pre: midFileURL, mifFileURL are not empty strings and progressBar is not null.
        // Post: midFileURL and mifFileURL have been loaded and an integer denoting 
        // success or otherwise has been returned.

        int LoadMapInfo(String midFileURL, String mifFileURL ,ProgressBar progressBar);

        // Property to get and set the value of the mapInfoDataFieldsToDisplay attribute
        // The getter returns the list of fields chosen by the user (can be empty and would be if // a KML file was used as the data source), the setter returns nothing.
        // Pre: True 
        // Post: The value of mapInfoDataFieldsToDisplay has been returned for getting or 
        // the value of mapInfoDataFieldsToDisplay has been set to the input value for
         // setting.

        List<String> MapInfoDataFieldsToDisplay {get; set;}

        // Property to get and set the value of the sociologicalDataChoice attribute
        // The getter returns the sociological display option (NONE, WINNING_PARTY or 
        // SEAT_SAFETY) currently set,  the setter returns nothing.
        // Pre: True for the getter, value is one of NONE, WINNING_PARTY or 
        // SEAT_SAFETY for the setter
        // Post: The value of sociologicalDataChoice has been returned for getting or 
        // the value of sociologicalDataChoice has been set to the input value for setting.

        SociologicalDataSelection SociologicalDataChoice {get; set;}
    }
}
