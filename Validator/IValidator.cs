using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Security.AccessControl;
using System.Xml.Schema;

namespace BlackCat
{
    public interface IValidator
    {

        //Check the file format.
        //pre: filePath != null && fileFormat != null
        //post: return true the required fileFormat is consistent with the file indicated by filePath..
        bool validationFileFomart(string filePath, string fileFormat);

        // Check the given drive for the sufficient space to write the file
        //Pre: drive exists and the string drive is not the empty string
        //Post: Returns true iff there is sufficient space.
        bool hasSufficientDiskSpace(String driveLetter);

        //Tests if the user has write permissions on the given folder
        //Pre : folderURL must exist and folderURL is not the empty string
        //Post : Returns true iff the folder path is writable for the current user.
        bool folderIsWritable(String folderURL);

        //Tests if the given folder path exists
        //Pre : folderURL not the empty string
        //Post : Returns true iff a folder can be found at the given path
        bool folderExists(String folderURL);

        //Tests if the given file path is readable
        //Pre : fileURL is not the empty string
        //Post : Returns true iff file is indeed readable
        bool fileIsReadable(String fileURL);

        //Tests if the given file exists
        //Pre : fileURL is not the empty string
        //Post : Returns true iff the file could be found at the givel location
        bool fileExists(String fileURL);

        /// <summary>
        /// Checks that the url is not too long to be written to by .NET.
        /// </summary>
        /// <param name="fileURL"></param>
        /// <returns></returns>
        bool urlLengthIsValid(String fileURL);

        /// <summary>
        /// Tests if the format of the file is valid. 
        /// That is, it has a format consistant with "Federal Election Results-Qld-2004.xls"
        /// 
        /// Pre : excelURL must exist and excelURL is not the empty string
        /// Post : Returns true iff file structure corresponds to that expected for an Excel file
        /// </summary>
        /// <param name="excelURL">The url of the excel file</param>
        /// <returns>true if the file is valid, else returns false</returns>
        bool validateFERQExcelFormat(String excelURL);

        /// <summary>
        /// Tests if the format of the file is valid. 
        /// That is, it has a format consistant with "Qld_FederalResults by Electorate-2004.xls"
        /// 
        /// Pre : excelURL must exist and excelURL is not the empty string
        /// Post : Returns true iff file structure corresponds to that expected for an Excel file
        /// </summary>
        /// <param name="excelURL">The url of the excel file</param>
        /// <returns>true if the file is valid, else returns false</returns>
        bool validateQFREExcelFormat(String excelURL);

        // Tests if the given .mid file has the correct format
        //Pre : midURL exists and midURL is not the empty string
        //Post : Returns true iff file structure corresponds to that expected for a .mid file.
        bool validateMidFormat(String midURL);

        //Tests if the given .mif file has the correct format
        //Pre : mifURLexists and mifURL is not the empty string
        //Post : Returns true iff file structure corresponds to that expected for a .mif file
        bool validateMifFormat(String mifURL);

        /// <summary>
        /// Checks if the mid and mif files match each other -
        /// that the columns count and data count match.
        /// </summary>
        /// <param name="mifURL"></param>
        /// <param name="midURL"></param>
        /// <returns></returns>
        bool validateMapInfoFormat(String mifURL, String midURL);

        //Tests if the given KML file has the correct format
        //Pre : kmlURL exists and kmlURL is not the empty string
        //Post : Returns true iff file structure corresponds to that expected for a .kml file
        bool validateKMLFormat(String kmlURL);
    }
}
