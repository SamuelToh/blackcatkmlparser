using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackCat
{
    //This class coordinates all the relevant event
    //for creating a new kml file
    //and enhancing an existing kml file

    //Relation: This class will talks to DataModelFactory (for creating 
    //dataModels,
    //talks to KMLCreator for constructing our KMLDataModel object 
    //Finally calling the dataModelFactory again for outputting of a .kml file by supplying
    //our KMLDataModel object 
    public class Controller : IController
    {
        private IKMLDataModel kmlModel;

        public Controller(IKMLDataModel kmlModel)
        {
            this.kmlModel = kmlModel;
        }

        //Loads the KML file fileURL into the system, displaying progress of the operation in the progress bar progressBar. Returns an integer denoting the result of the operation as follows:
        //value = 0 (No problems) 
        //value = 1 (file doesn’t exist)
        //value = 2 (file is not readable)
        //value = 3 (file is in the wrong format)
        //Pre: fileURL is not the empty string and progressBar is not null.
        //Post: fileURL has been loaded and an integer denoting success or otherwise has been returned.
        public int loadKML(String fileURL, ProgressBar progressBar) { return 0; }

        //Loads the MapInfo files midFileURL and mifFileURL into the system, displaying progress of the operation in the progress bar progressBar. Returns an integer denoting the result of the operation as follows:
        //value = 0 (No problems) 
        //value = 1 (file doesn’t exist)
        //value = 2 (file is not readable)
        //value = 3 (file is in the wrong format)
        //Pre: midFileURL, mifFileURL are not empty strings and progressBar is not null.
        //Post: midFileURL and mifFileURL have been loaded and an integer denoting success or otherwise has been returned.
        public int loadMapInfo(String midFileURL, String mifFileURL, ProgressBar progressBar) { return 0; }

        //Loads the Excel file fileURL into the system, displaying progress of the operation in the progress bar progressBar. Returns an integer denoting the result of the operation as follows:
        //value = 0 (No problems) 
        //value = 1 (file doesn’t exist)
        //value = 2 (file is not readable)
        //value = 3 (file is in the wrong format)
        //Pre: fileURL is not the empty string and progressBar is not null.
        //Post: fileURL have been loaded and an integer denoting success or otherwise has been returned.
        public int loadExcel(String fileURL, ProgressBar progressBar) { return 0; }

        //Returns a list of the data fields in the sociological data file that could be used to perform data linking on.
        //Pre: A sociological data file has been loaded into the system
        //Post: A list of the data fields present in the sociological data file has been returned
        public List<string> getSociologicalDataFields() { return new List<string>(); }

        //Returns a list of the data fields in the MapInfo data set that could be used to perform data linking on.
        //Pre: A MapInfo data set has been loaded into the system 
        //Post: A list of the data fields present in the MapInfo data set has been returned
        public List<string> getGeographicalDataFields() { return new List<string>(); }

        //Returns a list of the data fields in the KML data file that could be used to perform data linking on.
        //Pre: A KML data file has been loaded into the system 
        //Post: A list of the data fields present in the KML file has been returned
        public List<string> getKMLDataFields() 
        {
            return this.kmlModel.getDataFieldNames();
        }

        //Returns a boolean indicating whether the geographic and sociological data fields can be linked using the fields geoField and socField
        //Pre: geoField is not the empty string and socField is not the empty string 
        //Post: A boolean indicating whether the desired linking operation can occur has been returned.
        public bool canLink(String geoField, String socField) { return true; }

        //Returns a boolean indicating whether it is possible to link the supplied geographical and sociological data using the data fields supplied.
        //Pre: None of geoField, socialField are the empty string 
        //Post: True
        public bool linkGeographicalAndSocialData(String geoField, String socialField) { return true; }

        //Requests validation of a folder to ensure it exists, can be written to etc. Returns an integer denoting the result of the verification as follows:
        //value = 0 (Folder is OK) 
        //value = 1 (folder doesn’t exist)
        //value = 2 (folder is not writable for the current user)
        //value = 3 (folder doesn’t have enough room)
        //Pre: folderURL is not the empty string
        //Post: The status of the folder has been verified and an integer denoting the folder’s status has been returned.
        public int validateFolder(string folderURL) { return 0; }

        //Creates the KML file using the files previously supplied by the user, writing it to the location desired and displaying a progress bar updated as the operation proceeds. Returns an integer denoting success (0) or failure (1).
        //Note that as there is only ever one set of geographical data and sociological data loaded into the system at any time, it is not necessary to specify the names of the input files in the method signature.
        //Pre: outputFileURL is not the empty string and progressBar is not null
        //Post: A new KML file is written to outputFolderURL, created from the input files.
        public int generateKMLFile(String outputFileURL, ProgressBar progressBar) { return 0; } 

       
    }
}
