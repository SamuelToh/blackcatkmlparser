
namespace Controller
{
    using System;
    using System.IO;
    using ControllerInterface;
    using Input;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class KMLParserControl : IKMLParserControl
    {
        // Required to implement this class as a Singleton.
        private static KMLParserControl instance;

        //Holds the MapInfo files as a data model for future use.
        private GeographModel mapInfoModel;

        //Holds the Excel file as a data model for future use.
        private SocialModel excelModel;

        //Holds the user provided KML file as a data model for future use.
        private KMLModel inputKMLModel;

        //Holds the KMLModel generated from a user provided pair of MapInfo files as a data model for future use.
        private KMLModel outputKMLModel; 

        // Holds the name of the geographical data field to link on.
        private string geoLinkField;

        // Holds the name of the sociological data field to link on.
        private string socialLinkField;

        // No arguments protected constructor.

         public KMLParserControl KMLParserControl()
         {
             mapInfoModel = new GeographModel();
             excelModel = new SocialModel();
             inputKMLModel = new KMLModel();
             outputKMLModel = new KMLModel();
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

        // Returns a boolean indicating whether the geographic and sociological data fields can 
        // be linked using the fields geoField and socField.

        // Pre: geoField is not the empty string and socField is not the empty string. 
        // Post: A boolean indicating whether the desired linking operation can occur has been returned.

         public bool canLink(String geoField, String socField)
         {
             bool link;
             DataMerger merger = new DataMerger();

             // Work out which of the two geographical models is populated and call the appropriate method.
             // If mapInfoModel is null, then we have a KML Model as input, otherwise we have a GeographModel.

             if (mapInfoModel == null)
                 link = merger.canLink(inputKMLModel, geoField, excelModel, socField);
             else
                 link = merger.canLink(mapInfoModel, geoField, excelModel, socField);

             // If the test was successful, save the values of the linking columns for later use.

             if (link)
             {
                 geoLinkField = geoField;
                 socialLinkField = socField;
             }
             return link;
         }

        // Creates the KML file using the files previously supplied by the user, writing it to the 
        // location desired and displaying a progress bar updated as the operation proceeds. 
        // Returns an integer denoting success (0) or failure (1).

        // Pre: outputFileURL is not the empty string and progressBar is not null.
        // Post: A new KML file is written to outputFolderURL, created from the input files.

         public int generateKMLFile(String outputFileURL, ProgressBar progressBar)
         {
             // NOTE: THE PROGRESS BAR IS VERY CRUDELY INCREMENTED AT THIS STAGE. I STILL HAVE TO 
             // FINE TUNE HOW BEST TO MANIPULATE IT.
             // AT THIS STAGE I AM DIVIDING THE OPERATION INTO 3 PARTS AND TREATING THEM AS EQUALS.
             // I DON'T THINK THIS WILL PROVE TO BE THE CASE. BUT AS I AM CALLING METHODS, I AM NOT
             // SURE HOW ELSE TO INCREMENT.

             int errorCode = 0;
             ResourceWriter rWriter;
             StreamWriter writer;

             // Set up the progress bar.

             progressBar.Minimum = 0;
             progressBar.Maximum = 3;

             // First, if there is a GeographModel, convert it to a KMLModel, otherwise inputKMLModel
             // must be populated and we can point outputKMLModel at the input model.

             // Note that if we start out with a GeographModel, we won't be able to use the user provided
             // data column directly for linking as we have converted from the MapInfo format 
             // to the KML format by the time we get to this method. The field name in the KMLModel
             // is highly likely to be different to that of the original MapInfo file. So we need to 
             // pass the linking method the name of the field as it appears in the KMLModel, not as it
             // appeared in the GeographModel.

             // In reality, linking can only occur on fields that could form primary keys if the data were
             // stored in a database. In the KML file, there is only one field that conforms to this 
             // description and that is whatever appears inside the name tag that is within each placemark
             // tag. So the list of available data fields for the KMLModel should contain only one field and
             // that field is assumed to be the one that we know we can link on.

             if (mapInfoModel != null)
             {
                 List<String> KMLFields;

                 // Builds the KMLModel from the GeographModel, retrieves the associated 1 item list of
                 // data fields then sets geoLinkField as the first value in this list.

                 outputKMLModel.buildKMLModel(mapInfoModel);
                 KMLFields = outputKMLModel.getDataFieldNames();
                 geoLinkField = KMLFields[0];
             }
             else
                 outputKMLModel = inputKMLModel;

             // Increment the progress bar by one third.

             progressBar.Increment(1);

             // Link the data if required. The nested if attempts to link the data and set the error code
             // in one operation.

             if (excelModel != null)
             {
                 if (!(linkGeographicalAndSocialData(geoLinkField, socialLinkField)))
                 {
                     // 1 indicates a problem, so exit immediately.

                     errorCode = 1;
                     return errorCode;
                 }
                 // Otherwise no problem, so do nothing.
             }

             // Increment the progress bar by one third - now through 2/3 of the steps in the operation.

             progressBar.Increment(1);

            // If we reach here, all has gone well so far. Now we write the model to file.
            // First get a writer.
            // I have a funny feeling the writer will be restructured similarly to the reader.
            // However, for the moment, the given syntax has been used.

             rWriter = new ResourceWriter();
             writer = rWriter.getWriter(outputFileURL);

            // Give it to the outputKMLModel object so that it can produce a stream for the writer to write.

             outputKMLModel.write(writer);

             // Increment the progress bar by one third - now we are finished.

             progressBar.Increment(1);

             return errorCode;
         }

        // Returns a list of the data fields in the MapInfo data set that could be used to perform 
        // data linking on.

        // Pre: A MapInfo data set has been loaded into the system 
        // Post: A list of the data fields present in the MapInfo data set has been returned.

        public List<string> getGeographicalDataFields()
        {
            return mapInfoModel.DataFieldNames();
        }

        //Returns a list of the data fields in the KML data file that could be used to perform 
        // data linking on.

        // Pre: A KML data file has been loaded into the system 
        // Post: A list of the data fields present in the KML file has been returned.

        public List<string> getKMLDataFields()
        {
            return inputKMLModel.getDataFieldNames();
        }

        // Returns a list of the data fields in the sociological data file that could be used to 
        // perform data linking on.

        // Pre: A sociological data file has been loaded into the system
        // Post: A list of the data fields present in the sociological data file has been returned.

        public List<string> getSociologicalDataFields()
        {
            return excelModel.DataFields();
        }

        // Returns a boolean indicating whether it is possible to link the supplied geographical and 
        // sociological data using the data fields supplied.

        // Note, this does not need to be a publically available method. The logic in the UI is that we 
        // check IF we can link. The linking is not called for from the UI. It forms part of the call to
        // create the new file. Only this class needs to know about this method.

        // Pre: None of geoField, socialField are the empty string. 
        // Post: A boolean has been returned that indicates if it is possible to link the geographical
        // and sociological data in the manner indicated by the user.

        public bool linkGeographicalAndSocialData(String geoField, String socialField)
        {
            DataMerger merger;
            
            // Link the data.

            merger = new DataMerger();

            errorCode = merger.linkDataModels(outputKMLModel, geoField, excelModel, socialField);

            // Finally, return the result.

            if (errorCode == 0)
                return true;
            else
                return false;
        }

        // Loads the Excel file fileURL into the system, displaying progress of the operation in the 
        // progress bar progressBar. Returns an integer denoting the result of the operation as follows:

        // value = 0 (No problems) 
        // value = 1 (file doesn’t exist)
        // value = 2 (file is not readable)
        // value = 3 (file is in the wrong format).

        // Pre: fileURL is not the empty string and progressBar is not null.
        // Post: fileURL have been loaded and an integer denoting success or otherwise has been returned.

        public int loadExcel(String fileURL, ProgressBar progressBar)
        {
            int errorValue = 0;
            ResourceReader excelReader;

            // NOTE: This method needs to pass on the progress bar to the buildSocialModel method.
            // Only buildSocialModel can measure its own progress.

            // First test the validity of the Excel (.xls or .csv) file and exit if any problems are discovered.

            errorValue = validateFile(fileURL);

            if (errorValue != 0)
                return errorValue;

            // If we've made it this far, then the Excel file is O.K. and we can get a 
            // ResourceReader object for it.

            excelReader = new ResourceReader(fileURL);

            // Now we can build a SocialModel from this file.

            excelModel.buildSocialModel(excelReader);

            // And return the value 0 to denote success.

            return errorValue;
        }

        // Loads the KML file fileURL into the system, displaying progress of the operation in 
        // the progress bar progressBar. Returns an integer denoting the result of the operation 
        // as follows:

        // value = 0 (No problems) 
        // value = 1 (file doesn’t exist)
        // value = 2 (file is not readable)
        // value = 3 (file is in the wrong format).

        // Pre: fileURL is not the empty string and progressBar is not null.
        // Post: fileURL has been loaded and an integer denoting success or otherwise has been returned.

        public int loadKML(String fileURL, ProgressBar progressBar)
        {
            int errorValue = 0;
            ResourceReader kmlReader;

            // NOTE: This method needs to pass on the progress bar to the buildKMLModel method.
            // Only buildKMLModel can measure its own progress.

            // First test the validity of the .kml file and exit if any problems are discovered.

            errorValue = validateFile(fileURL);

            if (errorValue != 0)
                return errorValue;

            // If we've made it this far, then the KML file is O.K. and we can get a 
            // ResourceReader object for it.

            kmlReader = new ResourceReader(fileURL);

            // Now we can build a KMLModel from this file.

            inputKMLModel.buildKMLModel(kmlReader);

            // And return the value 0 to denote success.

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

        public int loadMapInfo(String midFileURL, String mifFileURL, ProgressBar progressBar)
        {
            int errorValue = 0;
            ResourceReader midReader;
            ResourceReader mifReader;

            // NOTE: This method needs to pass on the progress bar to the buildGeographModel method.
            // Only buildGeographModel can measure its own progress.

            // First test the validity of the .mid file and exit if any problems are discovered.

            errorValue = validateFile(midFileURL);

            if (errorValue != 0)
                return errorValue;

            // Now test the validity of the .mif file and exit if any problems are discovered.
            // Note that only 1, 2 or 3 is returned if there is a problem, we need to add 3 to
            // this value to indicate the problem is with the mif file not the mid file.

            errorValue = validateFile(mifFileURL);

            if (errorValue != 0)
                return errorValue + 3;

            // If we've made it this far, then both the mid and mif files are O.K. and we can get
            // ResourceReader objects for them.

            midReader = new ResourceReader(midFileURL);
            mifReader = new ResourceReader(mifFileURL);

            // Now we can build a GeographModel from these files.

            mapInfoModel.buildGeographModel(midReader, mifReader);

            // And return the value 0 to denote success.

            return errorValue;
        }

        // Requests validation of a folder to ensure it exists, can be written to etc. Returns an 
        // integer denoting the result of the verification as follows:

        // value = 0 (Folder is OK) 
        // value = 1 (folder doesn’t exist)
        // value = 2 (folder is not writable for the current user)
        // value = 3 (folder doesn’t have enough room).

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

            char driveName = folderURL.Substring(0, 1);

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

            // No more tests to run, if you get this far, you have passed all the tests and errorType is
            // still 0, so just return it.

            return errorType;
        }

//#########################################################################################################

        // Private helper methods for use with the publically available methods.

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

            passedTest = myValidator.fileIsReadable(folderURL);

            if (!passedTest)
            {
                errorType = 2;
                return errorType;
            }

            // Test 3: Check that the file is in the right format. If not, return a value of 3.

            switch (fileExtension.ToLower())
            {
                case "mid":
                    passedTest = myValidator.validateMidFormat(fileURL);
                    
                    break;

                case "mif":
                    passedTest = myValidator.validateMifFormat(fileURL);
                    break;

                case "kml":
                    passedTest = myValidator.validateKMLFormat(fileURL);
                    break;

                case "xls":
                case "csv":
                    passedTest = myValidator.validateExcelFormat(fileURL);
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
