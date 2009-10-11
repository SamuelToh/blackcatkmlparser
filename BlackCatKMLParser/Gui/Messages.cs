using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public static class Messages
    {
        //Local error messages
        public static string NO_MID_FILE_PATH = "You must enter a .mid file path";
        public static string NO_MIF_FILE_PATH = "You must enter a .mif file path";
        public static string NO_KML_FILE_PATH = "You must enter a .kml file path";
        public static string MID_UnReadable = "The .mid file can not be read";
        public static string MIF_UnReadable = "The mif file can not be read";
        public static string MIF_Format = "The mif file format is incorrect.";
        public static string MID_Format = "The mid file format is incorrect.";
        public static string KML_Format = "The kml file format is incorrect.";
        public static string NO_KML_UnReadable = "The KML file can not be read";
        public static string NO_EXCEL_FILE_PATH = "You must enter an Excel file path";
        public static string NO_OUTPUT_FILE_PATH = "You must enter an output file path";
        public static string MISSING_LINK_FIELD = "You must choose a value for both link fields";

        //Controller error messages
        public static string MAPINFO_LOAD_ERROR = "Mapinfo load error occurred";
        public static string EXCEL_LOAD_ERROR = "Excel load error occurred";
        public static string FIELD_LINK_ERROR = "The chosen data fields are not linkable.";
        public static string OUTPUT_FAILED = "Conversion failed";
        public static string GeoSoc_NoMatch = "Sorry, the loaded regions do not match the database sociological data.";
        //Folder Validation
        public static string OUTPUT_PATH_INVALID = "The output path is invalid";
        private static string FOLDER_PRE = "Output file error - ";
        public static string FOLDER_INVALID_1 = FOLDER_PRE + "folder doesn’t exist";
        public static string FOLDER_INVALID_2 = FOLDER_PRE + "folder is not writable for the current user";
        public static string FOLDER_INVALID_3 = FOLDER_PRE + "folder doesn’t have enough room";
        public static string FOLDER_INVALID_4 = FOLDER_PRE + "proposed path is too long for .NET to handle";
    }
}
