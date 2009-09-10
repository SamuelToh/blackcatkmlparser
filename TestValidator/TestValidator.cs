using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TestValidator
{
    [TestFixture] 
    public class TestValidator
    {
        //check the file type.
        //pre: filePath
        //post: 
        //bool validationFileType(string filePath, string fileType);

        // Check the given drive for the sufficient space to write the file
        //Pre: drive exists and the string drive is not the empty string
        //Post: Returns true iff there is sufficient space.
        //bool hasSufficientDiskSpace(String driveLetter);

        //Tests if the user has write permissions on the given folder
        //Pre : folderURL must exist and folderURL is not the empty string
        //Post : Returns true iff the folder path is writable for the current user.
        //bool folderIsWritable(String folderURL);

        //Tests if the given folder path exists
        //Pre : folderURL not the empty string
        //Post : Returns true iff a folder can be found at the given path
        //bool folderExists(String folderURL);

        //Tests if the given file path is readable
        //Pre : fileURL is not the empty string
        //Post : Returns true iff file is indeed readable
        //bool fileIsReadable(String fileURL);

        //Tests if the given file exists
        //Pre : fileURL is not the empty string
        //Post : Returns true iff the file could be found at the givel location
        //bool fileExists(String fileURL);

        //Tests if the given Excel file has the correct format
        //Pre : excelURL must exist and excelURL is not the empty string
        //Post : Returns true iff file structure corresponds to that expected for an Excel file
        //bool validateExcelFileFormat(String excelURL);
        //bool validateFERQExcelFormat(String excelURL);
        //bool validateQFREExcelFormat(String excelURL);

        // Tests if the given .mid file has the correct format
        //Pre : midURL exists and midURL is not the empty string
        //Post : Returns true iff file structure corresponds to that expected for a .mid file.
        //bool validateMidFormat(String midURL);

        //Tests if the given .mif file has the correct format
        //Pre : mifURLexists and mifURL is not the empty string
        //Post : Returns true iff file structure corresponds to that expected for a .mif file
        //bool validateMifFormat(String mifURL);

        // <summary>
        // Checks if the mid and mif files match each other -
        // that the columns count and data count match.
        // </summary>
        // <param name="mifURL"></param>
        // <param name="midURL"></param>
        // <returns></returns>
        //bool validateMapInfoFormat(String mifURL, String midURL);

        //Tests if the given KML file has the correct format
        //Pre : kmlURL exists and kmlURL is not the empty string
        //Post : Returns true iff file structure corresponds to that expected for a .kml file
        //bool validateKMLFormat(String kmlURL);
    }
}
