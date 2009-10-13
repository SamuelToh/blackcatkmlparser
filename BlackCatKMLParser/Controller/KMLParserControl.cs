using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using log4net;

namespace BlackCat
{
    public class KMLParserControl : IKMLParserControl
    {
        // Required to implement this class as a Singleton.
        private static KMLParserControl instance;

        // Holds the MapInfo or KML data file(s) as a data model for future use.
        private GeoModel geoModel;

        // Holds a reference to the SocialLogic object.
        private SocialLogic socialLogic;

        // Holds the list of MapInfo data fields that the user wants to display.
        private List<String> mapInfoDataFieldsToDisplay;

        // Holds the sociological data display choice of the user. 
        // Possible values are NONE, WINNING_PARTY or SEAT_SAFETY.
        private SociologicalDataSelection sociologicalDataChoice;

        // No arguments private constructor.

        private KMLParserControl()
        {
            geoModel = new GeoModel();
            socialLogic = new SocialLogic();
            mapInfoDataFieldsToDisplay = null;
            sociologicalDataChoice = SociologicalDataSelection.NONE;
        }

        // Special property needed to implement the Singleton design pattern. Either instantiates
        // the class if this has not occurred yet or returns a reference to the class if 
        // instantiation has occurred.

        public static KMLParserControl Instance()
        {
            if (instance == null)
                instance = new KMLParserControl();

            return instance;
        }

        // Checks to see if any sociological data can be added to the geographical data.
        // Returns a boolean – true if sociological data can be added, false if it can’t.
        // Pre: True
        // Post: A boolean has been returned indicating if sociological data can be added to 
        // this set of geographical data.

        public bool CanAddSociologicalData()
        {
            bool canAdd;

            if (geoModel == null)
                canAdd = false;
            else
                canAdd = socialLogic.CanMatchSociologicalData(geoModel);

            return canAdd;
        }

        // Creates the KML file using the files previously supplied by the user, writing it to the 
        // location desired and arranging the updating of a progress bar as the operation 
        // proceeds. 
        // Returns an integer denoting success (0) or failure (1).
        // Pre: outputFileURL is not the empty string and progressBar is not null
        // Post: A new KML file is written to outputFolderURL, created from the input files and
        // containing the list of MapInfo data fields chosen by the user.

        public int GenerateKMLFile(String outputFileURL, ProgressBar progressBar)
        {
            bool success;
            int returnValue;

            // First, add sociological data if it was requested. SociologicalDataChoice
            // of NONE indicates we do not need to add any sociological data.

            if (sociologicalDataChoice == SociologicalDataSelection.SEAT_SAFETY && geoModel != null)
            {
                // Add winning party information.

                socialLogic.CalculateSeatSafety(geoModel, true);

                // Add seat safety information.

                socialLogic.CalculateSeatWinners(geoModel, false);
            }
            else if (sociologicalDataChoice == SociologicalDataSelection.WINNING_PARTY && geoModel != null)
            {
                // Add winning party information.

                socialLogic.CalculateSeatSafety(geoModel, false);

                // Add seat safety information.

                socialLogic.CalculateSeatWinners(geoModel, true);
            }

            // Otherwise do nothing - if geoModel is null or user doesn't want any sociological data.

            // Add to the GeoModel information about which folder to write each region to.

            socialLogic.SetFederalDistricts(geoModel);

            // Next, we create a KMLWriter to perform the writing.

            KMLWriter writer = new KMLWriter();

            // Then pass it the model, destination, MapInfo fields to include and a ProgressBar to update.

            if (geoModel == null)
                success = false;
            else
                success = writer.WriteToFile(geoModel, mapInfoDataFieldsToDisplay, outputFileURL, progressBar);

            // Now work out the return value for the UI and return it.

            if (success == true)
                returnValue = 0;
            else
                returnValue = 1;

            return returnValue;
        }

        // Extracts the data fields from the MapInfo file so that the user can select one or more 
        // of them to display in Google Earth.
        // Returns a list of strings containing the names of the data fields present in the 
        // MapInfo file.
        // Pre: True
        // Post: A list of field names corresponding to those found in the MapInfo file is 
        // returned.

        public List<String> GetMapInfoDataFields()
        {
            if (geoModel == null)
                return null;
            else
                return geoModel.DataFieldNames;
        }

        // Loads the KML file fileURL into the system, arranging the updating of a progress 
        // bar as the operation proceeds. 
        // Returns an integer denoting success (0) or failure (1).
        // Pre: fileURL is not the empty string and progressBar is not null.
        // Post: fileURL has been loaded and an integer denoting success or otherwise has 
        // been returned.

        public int LoadKML(String fileURL, ProgressBar progressBar)
        {
            bool success;
            int returnValue;
            KMLReader reader;

            if (geoModel == null)
                success = false;
            else
            {
                // First up, obtain a KMLReader object to read in the data.

                reader = new KMLReader(fileURL);

                // Next, build the GeoModel using the reader just created.

                success = geoModel.BuildGeoModel(reader, progressBar);
            }

            // Now work out the return value for the UI and return it.

            if (success == true)
                returnValue = 0;
            else
                returnValue = 1;

            return returnValue;
        }

        // Loads the MapInfo files midFileURL and mifFileURL into the system, arranging 
        // the updating of a progress bar as the operation proceeds. 
        // Returns an integer denoting success (0) or failure (1).
        // Pre: midFileURL, mifFileURL are not empty strings and progressBar is not null.
        // Post: midFileURL and mifFileURL have been loaded and an integer denoting 
        // success or otherwise has been returned.

        public int LoadMapInfo(String midFileURL, String mifFileURL, ProgressBar progressBar)
        {
            bool success;
            int returnValue;
            MapInfoReader reader;

            if (geoModel == null)
                success = false;
            else
            {
                // First up, obtain a KMLReader object to read in the data.

                reader = new MapInfoReader(midFileURL, mifFileURL);

                // Next, build the GeoModel using the reader just created.

                success = geoModel.BuildGeoModel(reader, progressBar);
            }

            // Now work out the return value for the UI and return it.

            if (success == true)
                returnValue = 0;
            else
                returnValue = 1;

            return returnValue;
        }

        // Property to get and set the value of the mapInfoDataFieldsToDisplay attribute
        // The getter returns the list of fields chosen by the user (can be empty and would be if 
        // a KML file was used as the data source), the setter returns nothing.
        // Pre: True 
        // Post: The value of mapInfoDataFieldsToDisplay has been returned for getting or 
        // the value of mapInfoDataFieldsToDisplay has been set to the input value for
        // setting.

        public List<String> MapInfoDataFieldsToDisplay
        {
            get
            {
                return mapInfoDataFieldsToDisplay;
            }
            set
            {
                mapInfoDataFieldsToDisplay = value;
            }
        }

        // Property to get and set the value of the sociologicalDataChoice attribute
        // The getter returns the sociological display option (NONE, WINNING_PARTY or 
        // SEAT_SAFETY) currently set,  the setter returns nothing.
        // Pre: True for the getter, value is one of NONE, WINNING_PARTY or 
        // SEAT_SAFETY for the setter
        // Post: The value of sociologicalDataChoice has been returned for getting or 
        // the value of sociologicalDataChoice has been set to the input value for setting.

        public SociologicalDataSelection SociologicalDataChoice
        {
            get
            {
                return sociologicalDataChoice;
            }
            set
            {
                sociologicalDataChoice = value;
            }
        }
    }
}
