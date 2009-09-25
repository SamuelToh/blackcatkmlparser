using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;
using BlackCat;

namespace TestValidator
{
    [TestFixture] 
    public class TestValidator
    {
        private Validator validator = new Validator();
        /// <summary>
        /// Tests if the format of the file is valid. 
        /// That is, it has a format consistant with "Federal Election Results-Qld-2004.xls"
        /// 
        /// Pre : excelURL must exist and excelURL is not the empty string
        /// Post : Returns true iff file structure corresponds to that expected for an Excel file
        /// </summary>
        /// <param name="excelURL">The url of the excel file</param>
        /// <returns>true if the file is valid, else returns false</returns>
        //bool validateFERQExcelFormat(String excelURL);

        /// <summary>
        /// Tests if the format of the file is valid. 
        /// That is, it has a format consistant with "Qld_FederalResults by Electorate-2004.xls"
        /// 
        /// Pre : excelURL must exist and excelURL is not the empty string
        /// Post : Returns true iff file structure corresponds to that expected for an Excel file
        /// </summary>
        /// <param name="excelURL">The url of the excel file</param>
        /// <returns>true if the file is valid, else returns false</returns>
        //bool validateQFREExcelFormat(String excelURL);

        // Tests if the given .mid file has the correct format
        //Pre : midURL exists and midURL is not the empty string
        //Post : Returns true iff file structure corresponds to that expected for a .mid file.
        //bool validateMidFormat(String midURL);

        //Tests if the given .mif file has the correct format
        //Pre : mifURLexists and mifURL is not the empty string
        //Post : Returns true iff file structure corresponds to that expected for a .mif file
        //bool validateMifFormat(String mifURL);

        /// <summary>
        /// Checks if the mid and mif files match each other -
        /// that the columns count and data count match.
        /// </summary>
        /// <param name="mifURL"></param>
        /// <param name="midURL"></param>
        /// <returns></returns>
        //bool validateMapInfoFormat(String mifURL, String midURL);

        //Tests if the given KML file has the correct format
        //Pre : kmlURL exists and kmlURL is not the empty string
        //Post : Returns true iff file structure corresponds to that expected for a .kml file
        //bool validateKMLFormat(String kmlURL);
    }
}
