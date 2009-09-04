using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackCat
{

    //This class contains a list of methods to verify file/folder 
    //and native file format.
    public class ValidationCls : IValidator
    {
        const int MINIMUM_AMT_OF_DISKSPACE = 1000;
        //TODOs:

        public bool hasSufficientDiskSpace(String driveLetter) { return true; }

        public bool folderIsWritable(String folderURL) { return true; }

        public bool folderExists(String folderURL) { return true; }

        public bool fileIsReadable(String fileURL) { return true; }

        public bool fileExists(String fileURL) { return true; }

        public bool validateExcelFormat(String excelURL) { return true; }

        public bool validateMidFormat(String midURL) { return true; }

        public bool validateMifFormat(String mifURL) { return true; }

        public bool validateKMLFormat(String kmlURL) { return true; }
      
    }


}//END Validation Class

