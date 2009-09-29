using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using log4net;

namespace BlackCat
{    
    public class KMLParserControl : IKMLParserControl
    {
        private ILog log;

        // Required to implement this class as a Singleton.
        private static KMLParserControl instance;

        // Holds the MapInfo or KML data file(s) as a data model for future use.
        private IGeoModel geoModel;

        // Holds the Excel file as a data model for future use.
        private ISocialLogic socialLogic;

        // No arguments private constructor.

         private KMLParserControl()
         {
             //IGeoModel geoModel = new GeoModel();
             //ISocialModel excelModel = new SocialModel();
             //IDataMerger merger = new DataMerger();
             log = LogManager.GetLogger(this.ToString());
         }

         public void ClearFields()
         {
             log = LogManager.GetLogger(this.ToString());
             geoModel = null;
         }

        // Special property needed to implement the Singleton design pattern. Either instantiates
        // the class if this has not occurred yet or returns a reference to the class if 
        // instantiation has occurred.

         public static KMLParserControl Instance()
         {
             if (instance == null)
             {
                 instance = new KMLParserControl();
             }
             return instance;
         }

        // Creates the KML file using the files previously supplied by the user, writing it to the 
        // location desired and arranging the updating of a progress bar as the operation proceeds. 
        // Returns an integer denoting success (0) or failure (1).

        // Pre: outputFileURL is not the empty string and progressBar is not null.
        // Post: A new KML file is written to outputFolderURL, created from the input files.

         public int GenerateKMLFile(String outputFileURL, ProgressBar progressBar)
         {
             // I am still a little uncertain about what to do with the progress bar here, as 
             // there are two stages to generating the KML file - one link if required and two,
             // write it out. I am not sure how long the linking will take and whether it needs
             // to display a progress bar in its own right or whether we can get away with just 
             // updating when the GeoModel is being written to file.

             
             //int errorCode = 0;
              
             // Link the data if required. The nested if attempts to link the data and set the error code
             // in one operation.

             //TODO:
             /*
             if (socioModel != null)
             {
                 if (!(linkGeographicalAndSocialData(geoLinkField, socioLinkField)))
                 {
                     // 1 indicates a problem, so exit immediately.
                     log.Debug("Could not link files");
                     return 1;
                 }
             }*/

             // If we reach here, all has gone well so far. Now we write the model to file.         

             KMLWriter writer = new KMLWriter();
             if (writer.WriteToFile(geoModel, new List<String>(), outputFileURL, progressBar))
                 return 0;
             /*if (geoModel.OutputKML(outputFileURL, progressBar))
                 return 0;
             log.Debug("Output failed");*/
             return 1;
         }

         public bool CanAddSociologicalData()
         {
             //TODO: implement
             return true;
         }


        // Returns a list of the data fields in the geographical data file(s) that could be used to perform  
        // data linking on.

        // Pre: A geographical data set has been loaded into the system 
        // Post: A list of the data fields present in the geographical data set has been returned.

        public string[] GetMapInfoDataFields()
        {
            if (geoModel != null)
            {
                List<String> fields = geoModel.DataFieldNames;
                if (fields != null)
                    return fields.ToArray();
            }
            return null;
        }

        // Loads the KML file fileURL into the system as a GeoModel, displaying progress of the operation in 
        // the progress bar progressBar. Returns an integer denoting the result of the operation 
        // as follows:

        // value = 0 (No problems) 
        // value = 1 (file doesn’t exist)
        // value = 2 (file is not readable)
        // value = 3 (file is in the wrong format).

        // Pre: fileURL is not the empty string and progressBar is not null.
        // Post: fileURL has been loaded and an integer denoting success or otherwise has been returned.

        public int LoadKML(String fileURL, ProgressBar progressBar)
        {
            int errorValue = 0;
            GeoModel kmlData;

            
            // First test the validity of the .kml file and exit if any problems are discovered.

            errorValue = validateFile(fileURL);

            if (errorValue != 0)
                return errorValue;

            // If we've made it this far, then the KML file is O.K. and we can create a 
            // GeoModel from it.

            kmlData = new GeoModel();
            if(kmlData.BuildGeoModel(new KMLReader(fileURL), progressBar) == false)
                errorValue = 1;
            
            // If there were no problems, keep a reference to kmlData for later use.
            
            if(errorValue == 0)
                geoModel = kmlData;

            // Now return the value 0 to denote success or 1 for failure.
            return errorValue;
        }

        // Loads the MapInfo files midFileURL and mifFileURL into the system, displaying 
        // progress of the operation in the progress bar progressBar. Returns an integer 
        // denoting the result of the operation as follows:

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

        public int LoadMapInfo(String midFileURL, String mifFileURL, ProgressBar progressBar)
        {
            int errorValue = 0;
            
            GeoModel mapInfoData;
            // First test the validity of the .mid file and exit if any problems are discovered.

            //errorValue = validateFile(midFileURL);

            if (errorValue != 0)
                return errorValue;

            // Now test the validity of the .mif file and exit if any problems are discovered.
            // Note that only 1, 2 or 3 is returned if there is a problem, we need to add 3 to
            // this value to indicate the problem is with the mif file not the mid file.

            //errorValue = validateFile(mifFileURL);

            if (errorValue != 0)
                return errorValue + 3;

            // If we've made it this far, then both the mid and mif files are O.K. and we can build
            // a GeoModel from them.
            
            mapInfoData = new GeoModel();
            if (mapInfoData.BuildGeoModel(new MapInfoReader(midFileURL, mifFileURL), progressBar) == false)
                errorValue = 1;
            
            // If there were no problems, keep a reference to mapInfoData for later use.
            
            if(errorValue == 0)
                geoModel = mapInfoData;

            // Now return the value 0 to denote success or 1 for failure.
            
            return errorValue;
        }

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

        public int validateFolder(string folderURL)
        {
            Validator myValidator = new Validator();
            int errorType = 0;
            bool passedTest;

            // Get the drive letter, which is assumed to be one character long and the first
            // character in the path string, which is the full path string, not just the file name.

            string driveName = folderURL.Substring(0, 1);

            // Test 1: Check that the folder exists. If not, return a value of 1.

            passedTest = myValidator.folderExists(folderURL);

            if (!passedTest)
            {
                errorType = 1;
                return errorType;
            }
            
            // Test 2: Check that the folder is writable. If not, return a value of 2.

            passedTest = myValidator.folderIsWritable(folderURL);

            if (!passedTest)
            {
                errorType = 2;
                return errorType;
            }

            // Test 3: Check that there is enough room in the folder to write a KML file. If not, return a value of 3.

            passedTest = myValidator.hasSufficientDiskSpace(driveName);

            if (!passedTest)
            {
                errorType = 3;
                return errorType;
            }

            // Test 4: Check that the file path is short enough to be given to a writer without generating
            // a PathTooLong exception.

            passedTest = myValidator.urlLengthIsValid(folderURL);

            if (!passedTest)
            {
                errorType = 4;
                return errorType;
            }

            // No more tests to run, if you get this far, you have passed all the tests and errorType is
            // still 0, so just return it.

            return errorType;
        }

// Public methods added for testing purposes
        public IGeoModel GeologicalModel
        {
            get{return geoModel;}
            set { geoModel = value; }
        }
        public ISocialLogic SocialLogic
        {
            get { return socialLogic; }
            set { socialLogic = value; }
        }

//#########################################################################################################

        // Private helper methods for use with the publically available methods.

        // Pre: None of geoField, socialField are the empty string. 
        // Post: A boolean has been returned that indicates if it is possible to link the geographical
        // and sociological data in the manner indicated by the user.

        private bool linkGeographicalAndSocialData(String geoField, String socialField)
        {
            /*
            DataMerger merger;
            
            // Link the data.

            merger = new DataMerger();
            */
            int errorCode = 0;
            //TODO: errorCode = merger.linkDataModels(geoModel, geoLinkField, socioModel, socialField);

            // Finally, return the result.

            if (errorCode == 0)
                return true;
            else
                return false;
             
        }

        // Checks fileURL to ensure that it is a valid file, i.e. that it exists, is readable and in 
        // the right format. Returns 0 if all is well, 1 if file does not exist, 2 if it is not readable
        // or 3 if it is not in the right format.

        // Pre: fileURL is not the empty string.
        // Post: Returns an integer denoting which validation test failed, or 0 if all were successful.

        private int validateFile(String fileURL)
        {
            Validator myValidator = new Validator();
            int errorType = 0;
            bool passedTest;

            // Pull off all the characters that come after the dot, these will be treated as the 
            // file extension, assumes nothing about the length of the file extension, only
            // that the string ends in .file_extension.

            String fileExtension = fileURL.Substring(fileURL.LastIndexOf("."));

            // Test 1: Check that folder exists. If not, return a value of 1.

            passedTest = myValidator.fileExists(fileURL);

            if (!passedTest)
            {
                errorType = 1;
                return errorType;
            }

            // Test 2: Check that the folder is readable. If not, return a value of 2.

            passedTest = myValidator.fileIsReadable(fileURL);

            if (!passedTest)
            {
                errorType = 2;
                return errorType;
            }

            // Test 3: Check that the file is in the right format. If not, return a value of 3.

            switch (fileExtension.ToLower())
            {
                case ".mid":
                    passedTest = myValidator.validateMidFormat(fileURL);
                    // What is this?
                    //passedTest = myValidator.validateMidFormat(fileURL, "tempString");
                    break;

                case ".mif":
                    passedTest = myValidator.validateMifFormat(fileURL);
                    //passedTest = myValidator.validateMifFormat(fileURL, "tempString");
                    break;

                case ".kml":
                    passedTest = myValidator.validateKMLFormat(fileURL);
                    break;

                case ".xls":
                case ".csv":
                    //TODO: fix passedTest = myValidator.validateExcelFormat(fileURL);
                    break;

                default: 
                    passedTest = false;
                    break;
            }

            if (!passedTest)
            {
                errorType = 3;
                return errorType;
            }

            // If we've made it this far, the file exists, is readable and in the right format, so 
            // errorType is still 0, just return it.

            return errorType;
        }
    }
}
